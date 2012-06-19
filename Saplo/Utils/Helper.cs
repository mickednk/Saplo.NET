namespace Saplo.Utils
{
	/// <summary>
	///   Helper class for miscellaneous tasks.
	/// </summary>
	public class Helper
	{
		private static int _currentId;

		/// <summary>
		///   Get the current id, or rather the last generated.
		/// </summary>
		/// <returns> </returns>
		public static int GetCurrentId()
		{
			return _currentId;
		}

		/// <summary>
		///   Gets a fresh id for method declaration, used for request against Saplo.
		/// </summary>
		/// <returns> </returns>
		public static int GetNextId()
		{
			return ++_currentId;
		}
	}
}