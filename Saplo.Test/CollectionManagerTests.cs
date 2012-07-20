using System.Configuration;
using NUnit.Framework;

namespace Saplo.Test
{
	[TestFixture]
	public class CollectionManagerTests
	{
		private SaploManager _saploManager;
		private int _testCollectionId;

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

		[Test]
		public void CreateCollectionTest()
		{
			var collection = _saploManager.Collections.Create("Test collection", "en", "Description for test collection");

			Assert.IsNotNull(collection);
			Assert.Greater(collection.CollectionID, 0);
			Assert.IsNotNullOrEmpty(collection.Name);
			Assert.IsNotNullOrEmpty(collection.Description);
			Assert.IsNotNullOrEmpty(collection.Language);

			_testCollectionId = collection.CollectionID;
		}

		[Test]
		public void DeleteCollectionTest()
		{
			
		}
	}
}