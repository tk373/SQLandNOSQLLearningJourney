package ch.informatik.roommanager;
import com.mongodb.client.MongoClient;
import com.mongodb.client.MongoClients;
import com.mongodb.client.MongoCollection;
import com.mongodb.client.MongoDatabase;
import org.bson.Document;
import static com.mongodb.client.model.Filters.eq;
import org.bson.Document;
import org.bson.conversions.Bson;
import com.mongodb.client.MongoClient;
import com.mongodb.client.MongoClients;
import com.mongodb.client.MongoCollection;
import com.mongodb.client.MongoDatabase;
import com.mongodb.client.model.Projections;
import com.mongodb.client.model.Sorts;


import javax.faces.annotation.ManagedProperty;
import javax.faces.annotation.SessionMap;
import javax.faces.application.FacesMessage;
import javax.faces.bean.ManagedBean;
import javax.faces.bean.RequestScoped;
import javax.faces.bean.SessionScoped;
import javax.faces.validator.ValidatorException;
import javax.faces.view.ViewScoped;
import java.io.Console;
import java.io.Serializable;
import java.security.Key;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;
import java.util.Random;

@ManagedBean
@SessionScoped
public class IndexBean{

    private String Key;

    MongoClient mongoClient = MongoClients.create("mongodb://localhost:27017");
    MongoDatabase database = mongoClient.getDatabase("Termine");
    MongoCollection<Document> collection1 = database.getCollection("Keys");


    public String weiterleitung(){
        MongoCollection<Document> collection = database.getCollection("termin");
        List<String> publickeys = collection.distinct("PublicKey", String.class).into(new ArrayList<>());
        List<String> privatekeys = collection.distinct("PrivateKey", String.class).into(new ArrayList<>());

        Document termin = new Document()
                .append("Key", Key);
        Document lol = new Document();

        collection1.deleteMany(lol);
        collection1.insertOne(termin);

        if (publickeys.contains(Key)) {
            return "/seeMeeting.xhtml";
        } else if (privatekeys.contains(Key)) {
            return "/editMeeting.xhtml";
        }
        return "index.xhtml";
    }

    public String getKey() {
        return Key;
    }

    public void setKey(String key) {
        Key = key;
    }
}