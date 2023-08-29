package ch.informatik.roommanager;
import com.fasterxml.jackson.annotation.JsonIgnoreProperties;
import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.DeserializationFeature;
import com.google.gson.Gson;
import com.mongodb.BasicDBObject;
import com.mongodb.ConnectionString;
import com.mongodb.MongoClientSettings;
import com.mongodb.client.MongoClient;
import com.mongodb.client.MongoClients;
import com.mongodb.client.MongoCollection;
import com.mongodb.client.MongoDatabase;
import com.mongodb.client.model.Filters;
import com.mongodb.client.result.UpdateResult;
import com.sun.faces.util.Json;
import org.bson.Document;
import com.fasterxml.jackson.databind.ObjectMapper;

import static com.mongodb.client.model.Filters.*;
import static org.bson.codecs.configuration.CodecRegistries.fromProviders;
import static org.bson.codecs.configuration.CodecRegistries.fromRegistries;

import org.bson.conversions.Bson;
import org.bson.json.JsonMode;
import org.bson.json.JsonReader;
import org.bson.json.JsonWriterSettings;
import org.bson.types.ObjectId;
import scala.util.parsing.json.JSONObject;

import javax.faces.application.FacesMessage;
import javax.faces.bean.ManagedBean;
import javax.faces.bean.SessionScoped;
import javax.faces.component.UIComponent;
import javax.faces.context.FacesContext;
import javax.faces.validator.FacesValidator;
import javax.faces.validator.Validator;
import javax.faces.validator.ValidatorException;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.Random;

@ManagedBean
@SessionScoped
public class TerminBearbeitenBean {
    private String PrivateKey;
    private Termin termin;
    private ObjectId id;

    public TerminBearbeitenBean() {

    }

    public String getPrivateKey() {
        return PrivateKey;
    }

    public void setPrivateKey(String privateKey) {
        PrivateKey = privateKey;
    }

    public Termin getTermin(){
        return termin;
    }

    public Termin findTermin(){
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
        PrivateKey = jsonReader2.readString();
        jsonReader2.readEndDocument();
        jsonReader2.close();

        Document doc = collection.find(eq("PrivateKey",PrivateKey)).first();
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
        termin1.setDatum(date);
        termin1.setVon(von);
        termin1.setBis(bis);
        termin1.setZimmer(zimmer);
        termin1.setTeilnehmer(teilnehmer);
        termin1.setBemerkung(bemerkung);
        termin1.setPrivatekey(privatekey);
        termin1.setPublickey(publickey);

        termin = termin1;
        return termin;
    }

    public String addEdditmeeting(){
            MongoClient mongoClient = MongoClients.create("mongodb://localhost:27017");
            MongoDatabase database = mongoClient.getDatabase("Termine");
            MongoCollection<Document> collection = database.getCollection("termin");
            Termin termin1 = new Termin();
            termin1 = termin;

        // Create a new document to insert into the collection
        Document doc = new Document()
                .append("Datum", termin1.getDatum())
                .append("Von", termin1.getVon())
                .append("Bis", termin1.getBis())
                .append("Zimmer", termin1.getZimmer())
                .append("Bemerkung", termin1.getBemerkung())
                .append("Teilnehmer", termin1.getTeilnehmer())
                .append("PrivateKey", PrivateKey)
                .append("PublicKey", termin1.getPublickey());


            Bson Filter = Filters.eq("PrivateKey", PrivateKey);
            UpdateResult result = collection.replaceOne(Filter, doc);

        return "/index.xhtml";
    }

    public void setTermin(Termin termin) {
        this.termin = termin;
    }
}

