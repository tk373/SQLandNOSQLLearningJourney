package ch.informatik.roommanager;

import com.mongodb.client.MongoClient;
import com.mongodb.client.MongoClients;
import com.mongodb.client.MongoCollection;
import com.mongodb.client.MongoDatabase;
import org.bson.Document;

import javax.faces.application.FacesMessage;
import javax.faces.bean.ManagedBean;
import javax.faces.bean.SessionScoped;
import javax.faces.component.UIComponent;
import javax.faces.context.FacesContext;
import javax.faces.validator.FacesValidator;
import javax.faces.validator.Validator;
import javax.faces.validator.ValidatorException;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;

@ManagedBean
@SessionScoped
@FacesValidator("publicKeyValidator")
public class PrivateKeyValidator implements Validator {

    MongoClient mongoClient = MongoClients.create("mongodb://localhost:27017");
    MongoDatabase database = mongoClient.getDatabase("Termine");

    @Override
    public void validate(FacesContext context, UIComponent component, Object value) throws ValidatorException {
        String key = (String) value;

        // Retrieve all PrivateKeys from the "termin" collection in the "Termine" database
        MongoCollection<Document> collection = database.getCollection("termin");
        List<String> privateKeys = collection.distinct("PrivateKey", String.class).into(new ArrayList<>());
        List<String> publicKey = collection.distinct("PublicKey", String.class).into(new ArrayList<>());

        // Check if the user input exists in the collection
        if (!privateKeys.contains(key) && !publicKey.contains(key)) {
            FacesMessage msg = new FacesMessage("Invalid Key.");
            msg.setSeverity(FacesMessage.SEVERITY_ERROR);

            throw new ValidatorException(msg);
        }
    }

}
