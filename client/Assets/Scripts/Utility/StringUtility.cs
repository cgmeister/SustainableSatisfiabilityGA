using UnityEngine;
using System.Collections;
using System;

public class StringUtility {

	public static string FilterStringInput(string str, string inpStr){
		string tempStr = "";
		string newStr = str;
		int inpLen = inpStr.Length;
		if (str == ""){
			return "0";
		}

		if (inpStr == ""  
		    || inpStr == "\r" 
		    || inpStr == "\b"){
			return str;
		} else if (str[Math.Max(0, str.Length - inpLen - 1)] == '0' && inpLen > 0){
			Debug.Log ("Has zero");
			tempStr = "0";
			return tempStr;
		}
	
		for (int i=0; i<inpLen; i++){
			char ch = inpStr [i];
			if (!Char.IsDigit(ch)){
				tempStr = newStr.Remove(Math.Max(0, (newStr.Length - 1) - (inpLen - 1)), Math.Max(0, inpLen));
				if (tempStr == ""){
					tempStr = "0";
				}
				return tempStr;
			}
		}
		Debug.Log (newStr);
		return newStr;
	}
}
