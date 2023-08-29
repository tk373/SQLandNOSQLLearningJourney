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





import javax.faces.bean.ManagedBean;
import javax.faces.bean.SessionScoped;
import java.util.Date;
import java.util.Random;

@ManagedBean
@SessionScoped
public class TerminListeBean {
    private TerminListe TerminListe;
    private Termin aktuell;


    public TerminListeBean() {
        this.TerminListe = new TerminListe();
        aktuell = new Termin();
    }

    public Termin getAktuell() {
        return aktuell;
    }

    public void setAktuell(Termin aktuell) {
        this.aktuell = aktuell;
    }

    public String addTermin() {

        try {
            // Connect to the MongoDB server
            MongoClient mongoClient = MongoClients.create("mongodb://localhost:27017");

            // Access the "Termine" database and "termin" collection
            MongoDatabase database = mongoClient.getDatabase("Termine");
            MongoCollection<Document> collection = database.getCollection("termin");

            // Generate a private key and public key
            Random rand = new Random();
            String privateKey = String.format("%05d", rand.nextInt(100000));
            String publicKey = String.format("%05d", rand.nextInt(100000));

            // Create a new document to insert into the collection
            Document termin = new Document()
                    .append("Datum", aktuell.getDatum())
                    .append("Von", aktuell.getVon())
                    .append("Bis", aktuell.getBis())
                    .append("Zimmer", aktuell.getZimmer())
                    .append("Bemerkung", aktuell.getBemerkung())
                    .append("Teilnehmer", aktuell.getTeilnehmer())
                    .append("PrivateKey", privateKey)
                    .append("PublicKey", publicKey);

            collection.insertOne(termin);

        } catch (Exception e) {
            System.err.println(e.getClass().getName() + ": " + e.getMessage());
        }

        TerminListe.add(aktuell);
        aktuell = new Termin();
        return "index.xhtml";
    }


    public TerminListe getTerminListe() {
        return TerminListe;
    }

    public void setTerminListe(TerminListe TerminListe) {
        this.TerminListe = TerminListe;
    }
}
