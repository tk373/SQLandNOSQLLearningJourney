package ch.informatik.roommanager;
import com.mongodb.client.MongoClient;
import com.mongodb.client.MongoClients;
import com.mongodb.client.MongoCollection;
import com.mongodb.client.MongoDatabase;
import org.bson.Document;

import static com.mongodb.client.model.Filters.all;
import static com.mongodb.client.model.Filters.eq;
import static org.bson.codecs.configuration.CodecRegistries.fromProviders;
import static org.bson.codecs.configuration.CodecRegistries.fromRegistries;

import org.bson.json.JsonMode;
import org.bson.json.JsonReader;
import org.bson.json.JsonWriterSettings;
import org.bson.types.ObjectId;

import javax.faces.annotation.ManagedProperty;
import javax.faces.bean.ManagedBean;
import javax.faces.bean.RequestScoped;
import javax.faces.bean.SessionScoped;
import java.io.Serializable;
import java.security.Key;
import java.util.Date;

@ManagedBean
@SessionScoped
public class TerminAnsehenBean{

    private String key;
    private Termin termin;
    private ObjectId id;

    public String getKey() {
        return key;
    }

    public void setKey(String key) {
        this.key = key;
    }

    public Termin getTermin() {
        MongoClient mongoClient = MongoClients.create("mongodb://localhost:27017");
        MongoDatabase database = mongoClient.getDatabase("Termine");
        MongoCollection<Document> collection = database.getCollection("termin");
        MongoCollection<Document> collection2 = database.getCollection("Keys");
        Termin termin1 = new Termin();

        Document doc2 = collection2.find().first();
        JsonWriterSettings settings2 = JsonWriterSettings.builder().outputMode(JsonMode.RELAXED).build();
        JsonReader jsonReader2 = new JsonReader(doc2.toJson(settings2));
        jsonReader2.readStartDocument();
        jsonReader2.readName("_id");
        id = jsonReader2.readObjectId();
        jsonReader2.readName("Key");
        key = jsonReader2.readString();
        jsonReader2.readEndDocument();
        jsonReader2.close();
        Document doc = collection.find(eq("PublicKey", key)).first();
        JsonWriterSettings settings = JsonWriterSettings.builder().outputMode(JsonMode.RELAXED).build();
        JsonReader jsonReader = new JsonReader(doc.toJson(settings));
        jsonReader.readStartDocument();
        jsonReader.readName("_id");
        ObjectId id = jsonReader.readObjectId();
        jsonReader.readName("Datum");
        Long datelong = jsonReader.readDateTime();
        Date date = new Date(datelong);
        jsonReader.readName("Von");
        String von = jsonReader.readString();
        jsonReader.readName("Bis");
        String bis = jsonReader.readString();
        jsonReader.readName("Zimmer");
        Integer zimmer = jsonReader.readInt32();
        jsonReader.readName("Bemerkung");
        String bemerkung = jsonReader.readString();
        jsonReader.readName("Teilnehmer");
        String teilnehmer = jsonReader.readString();
        jsonReader.readName("PrivateKey");
        String privatekey = jsonReader.readString();
        jsonReader.readName("PublicKey");
        String publickey = jsonReader.readString();
        jsonReader.readEndDocument();
        jsonReader.close();
        termin1.set_id(id);
        termin1.setVon(von);
        termin1.setBis(bis);
        termin1.setDatum(date);
        termin1.setZimmer(zimmer);
        termin1.setTeilnehmer(teilnehmer);
        termin1.setBemerkung(bemerkung);
        termin1.setPrivatekey(privatekey);
        termin1.setPublickey(publickey);

        return termin1;
    }

    public void setTermin(Termin termin) {
        this.termin = termin;
    }

}
