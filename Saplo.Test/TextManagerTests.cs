using System.Configuration;
using NUnit.Framework;

namespace Saplo.Test
{
	[TestFixture]
	public class TextManagerTests
	{
		private SaploManager _saploManager;

		[TestFixtureSetUp]
		public void SetupTests()
		{
			_saploManager = new SaploManager(ConfigurationManager.AppSettings["apikey"], ConfigurationManager.AppSettings["secretkey"]);
		}

		[TestFixtureTearDown]
		public void TearDownTests()
		{
			_saploManager.InvalidateToken();
			_saploManager = null;
		}
	}
}