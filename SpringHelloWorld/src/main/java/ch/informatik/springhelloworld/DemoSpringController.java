package ch.informatik.springhelloworld;


import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
public class DemoSpringController {
    @RequestMapping
    public String helloString(){
        return "Hello World from Spring root";
    }

}
