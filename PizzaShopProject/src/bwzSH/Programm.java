package bwzSH;

import org.bson.BsonDocument;
import org.bson.BsonInt64;
import org.bson.Document;
import org.bson.conversions.Bson;
import com.mongodb.MongoClientSettings;
import com.mongodb.MongoException;
import com.mongodb.client.MongoClient;
import com.mongodb.client.MongoClients;
import com.mongodb.client.MongoDatabase;
import com.mongodb.MongoCredential;

public class Programm {
    public static void main(String args[]){

        String url = "mongodb://admin:admin@127.0.0.1:27017/";

        try (MongoClient mongoClient = MongoClients.create(url)) {
            MongoDatabase database = mongoClient.getDatabase("ShopDb");

            try {
                Bson command = new BsonDocument("ping", new BsonInt64(1));
                Document commandResult = database.runCommand(command);
                System.out.println("Connected successfully to server.");
            } catch (MongoException me) {
                System.err.println("An error occurred while attempting to run a command: " + me);
            }
        }



        MongoCredential credential;
        /*
        credential = MongoCredential.createCredential("admin", "ShopDb",
                "admin".toCharArray());
        */

        System.out.println("Connected to the database successfully");

        /*
        for (var doc: dbs ) {
            System.out.println(doc);
        }
        */

        System.out.println("Collection created successfully");
    }
}
