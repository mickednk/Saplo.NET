Saplo.NET
=========

API wrapper for Saplo

Quickstart
----------

	// Connect to the Saplo-API
	var client = new SaploManager("API_KEY", "SECRET_KEY");
	
	// Create a new collection and store it in the API
	Collection myCollection = client.Collections.Create("My Collection Name", "en", "A description of this collection");
	
	// After a collection is successfully created, it is populated with an ID 
	int collectionId = myCollection.CollectionID;
	
	//// Create a manager to handle Text
	//SaploTextManager textMgr = new SaploTextManager(client);
	
	// Create and save new Text
	Text myText = client.Texts.Create(collectionId, "Body of My Text, but more meaningful");
	
	// After a text is successfully created, it is populated with an ID
	int textId = myText.TextID;
	
	// Extract Tags from your text.
	Tag[] myTags = client.Texts.Tags(collectionId, textId);
	
	// Print out the tags extracted
	foreach (var tag in myTags)
	{
		Console.WriteLine("Category: \"{0}\"\tTag: \"{1}\"", tag.Category, tag.Name);
	}

For the rest of the API methods and examples, refer to http://developer.saplo.com/