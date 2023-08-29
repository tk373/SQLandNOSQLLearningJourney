package ch.informatik.roommanager;

import javax.faces.application.FacesMessage;
import javax.faces.component.UIComponent;
import javax.faces.context.FacesContext;
import javax.faces.validator.FacesValidator;
import javax.faces.validator.Validator;
import javax.faces.validator.ValidatorException;
import java.util.Date;

@FacesValidator("futureDateValidator")
public class FutureDateValidator implements Validator {

    @Override
    public void validate(FacesContext context, UIComponent component, Object value) throws ValidatorException {
        Date inputDate = (Date) value;
        Date currentDate = new Date();

        if (inputDate.compareTo(currentDate) <= 0) {
            FacesMessage msg = new FacesMessage("Please enter a future date.");
            msg.setSeverity(FacesMessage.SEVERITY_ERROR);
            throw new ValidatorException(msg);
        }
    }

}
