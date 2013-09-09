using System;
using System.Collections;

namespace Optimera.GA
{
	/// <summary>
	/// Compares genomes by fitness
	/// </summary>
	public sealed class GenomeComparer : IComparer
	{
        /// <summary>
        /// 
        /// </summary>
		public GenomeComparer()
		{
		}


        /// <summary>
        /// Compares two genomes by fitness.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare( object x, object y)
		{
			if ( !(x is Genome) || !(y is Genome))
				throw new ArgumentException("Not of type Genome");

			if (((Genome) x).Fitness > ((Genome) y).Fitness)
				return 1;
			else if (((Genome) x).Fitness == ((Genome) y).Fitness)
				return 0;
			else
				return -1;
		}
	}
}
