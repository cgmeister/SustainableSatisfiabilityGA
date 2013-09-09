using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	[SerializeField]
	private GameObject playerPrefab = null;

	[SerializeField]
	private Transform projectilePrefab = null;
	
	private int _playerHorizontalSpeed = 0;
	private int _projectileSpeed = 0;
	private int _rateOfFire = 0;
	private int _degree = 0;
	private float _offset = 0;
	private float _moveRate = 0;
	private float _reqNum = 0;

	private bool _canShoot = true;
	private float _totalTime = 0f;

	public Transform container = null;
	public Transform projectileContainer = null;
	private GameObject _player = null;

	private Vector3 _playerPosCache = Vector3.zero;
	private Vector3 _projectilePosCache = Vector3.zero;

	private bool _isEnabled = true;
	private int laneIndex = 1;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (_isEnabled){
			// For testing only
			SetPlayerControls ();
			_totalTime += Time.deltaTime;

			if (_totalTime >= _rateOfFire) {
				_canShoot = true;
			} else {
				_canShoot = false;
			}
		}
	}

	public void IsPaused(bool value){
		_isEnabled = value;
		int len = projectileContainer.childCount;
		for (int i=0; i<len; i++){
			Projectile projectile = projectileContainer.GetChild(i).GetComponent<Projectile>();
			projectile.isEnabled = value;
		}
	}

	public void UpdateData(SimulationModel simModel, float offset, float moveRate){
		_playerHorizontalSpeed = simModel.horizontalMoveSpeed;
		_projectileSpeed = simModel.projectileSpeed;
		_rateOfFire = simModel.rateOfFire;
		_reqNum = simModel.reqNum;
		_degree = simModel.reqDegree;
		_offset = offset;
		_moveRate = moveRate;
	}

	private void SetPlayerControls(){
		if (Input.GetKeyDown(KeyCode.Space)){
			ShootProjectile (_projectileSpeed);
		} else if (Input.GetKeyDown(KeyCode.D)){
			Move (_moveRate);
		} else if (Input.GetKeyDown(KeyCode.A)){
			Move (-_moveRate);
		}
	}

	public void InitializePlayer(){
		if (_player == null) {
			_player = (GameObject)Instantiate (playerPrefab, Vector3.zero, Quaternion.identity);
		}

		//Vector3 playerScale = new Vector3(offset, offset, offset);
		_player.transform.parent = container;
		_player.transform.localScale = Vector3.one;
		_playerPosCache = new Vector3 (_offset * 1.5f, -_degree * _offset, -0.01f);
		_player.transform.localPosition = _playerPosCache;
	}
	
	public void ShootProjectile (int speed){
		if (_canShoot){
			_totalTime = 0;
			GameObject bullet = (GameObject) Instantiate (projectilePrefab.gameObject, Vector3.zero, Quaternion.identity);				
			bullet.transform.parent = projectileContainer;
			_projectilePosCache = _player.transform.localPosition;
			_projectilePosCache.y += _offset/2f;
			bullet.transform.localPosition = _projectilePosCache;
			Projectile projectile = bullet.GetComponent<Projectile>();
			projectile.container = projectileContainer;
			projectile.rate = _projectileSpeed;
			projectile.laneIndex = laneIndex;
			projectile.offset = _offset;
			projectile.isEnabled = true;
		}
	}

	public void Move(float offset){

		float direction = offset * 2f;
		float newPositionX = _player.transform.localPosition.x + direction;
		if (newPositionX > 0 && newPositionX < Mathf.Abs(offset) * ((_reqNum * 2) + 1.1f)){
			_playerPosCache.x = newPositionX;
			_player.transform.localPosition = _playerPosCache;
			laneIndex = offset < 0 ? laneIndex-1 : laneIndex+1;
		}
	}

	public void RemoveProjectileContents(){
		int len = projectileContainer.childCount;
		for (int i=len-1; i>=0; i--){
			Destroy (projectileContainer.GetChild (i).gameObject);
		}
	}
}
