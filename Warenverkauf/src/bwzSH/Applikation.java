package bwzSH;
import java.sql.*;
import java.util.Scanner;

public class Applikation {
    public static String dbUrl = "jdbc:mysql://localhost/classicmodels?useUnicode=true&useJDBCCompliantTimezoneShift=true &useLegacyDatetimeCode=false&serverTimezone=UTC";
    public static String username = "root";
    public static String password = "pipikaka";


    public static void main(String[] args) throws SQLException {

        ApplicationRun();

    }

        public static void ApplicationRun() throws SQLException {

            boolean Continue = true;

            while(Continue == true) {

                Menu();

                SwitchCaseOptions();
            }
        }

    public static int ChooseOption(){
        Scanner myObj = new Scanner(System.in);
        String option = myObj.nextLine();
        int optionsChosen = Integer.parseInt(option);
        return optionsChosen;
    }

    public static void SwitchCaseOptions() throws SQLException {
        int optionsChosen = ChooseOption();

        switch (optionsChosen) {
            case 1:
                ShowProducts();
                break;
            case 2:
                ShowProducsOfLine();
                break;
            case 3:
                ShowCustomer();
                break;
            case 4:
                SearchCustomer();
                break;
            case 5:
                AddProduct();
                break;
            case 6:
                ChangeProduct();
                break;
            case 7:
                Deleteproduct();
                break;
            case 8:
                EndProgramm();
                break;
            default:
                System.out.println("Option nicht erkannt, bitte nur Nummern von 1-9 eingeben!");
        }

    }

    public static void Menu(){
            System.out.println("==================================");
            System.out.println("Warenverkauf");
            System.out.println("==================================");
            System.out.println("");
            System.out.println("1.  Produkte Anzeigen:");
            System.out.println("2.  Produkte einer Produktelinie anzeigen");
            System.out.println("3.  Kunden Anzeigen");
            System.out.println("4.  Kunden Suchen");
            System.out.println("5.  Add Product");
            System.out.println("6.  Change Product");
            System.out.println("7.  Delete Product");
            System.out.println("8.  End Programm");
        }



        public static void EndProgramm(){
            System.exit(0);
        }
        public static void ShowProducts() throws SQLException {
            Connection conn = DriverManager.getConnection(dbUrl, username, password);

            Statement statement = conn.createStatement();
            String sqlStatement = "SELECT productCode,productName,productLine,productVendor,quantityInStock,buyPrice FROM products";
            ResultSet resultSet = statement.executeQuery(sqlStatement);
            while(resultSet.next()) {
                // Daten auslesen mittels Feldname
                String productCode = resultSet.getString("productCode");
                String productName = resultSet.getString("productName");
                String productLine = resultSet.getString("productLine");
                String productVendor = resultSet.getString("productVendor");
                short quantityInStock = resultSet.getShort("quantityInStock");
                double buyPrice = resultSet.getDouble("buyPrice");
                System.out.println("ProductCode: " + productCode);
                System.out.println("Product Name: " + productName);
                System.out.println("Product Line: " + productLine);
                System.out.println("Product Vendor: " + productVendor);
                System.out.println("Quantity InStock" + quantityInStock);
                System.out.println("Buy Price: " + buyPrice);
                System.out.println("");
            }
            conn.close();
        }
        public static void ShowProducsOfLine() throws SQLException {

            Connection conn = DriverManager.getConnection(dbUrl, username, password);

            System.out.println("");
            Scanner myOb2j = new Scanner(System.in);
            System.out.println("Produktlinie Angeben:");

            String myOb2jv = myOb2j.nextLine();  // Read user input
            String sqlStatement2 = "SELECT productCode,productName,productLine,productVendor,quantityInStock,buyPrice FROM products where productline = ?";
            PreparedStatement prepStatemt = conn.prepareStatement(sqlStatement2);
            prepStatemt.setString(1, myOb2jv);
            ResultSet d = prepStatemt.executeQuery();
            while(d.next()) {
                String productCode2 = d.getString("productCode");
                String productName2 = d.getString("productName");
                String productLine2 = d.getString("productLine");
                String productVendor2 = d.getString("productVendor");
                short quantityInStock2 = d.getShort("quantityInStock");
                double buyPrice2 = d.getDouble("buyPrice");
                System.out.println("ProductCode: " + productCode2);
                System.out.println("Product Name: " + productName2);
                System.out.println("Product Line: " + productLine2);
                System.out.println("Product Vendor: " + productVendor2);
                System.out.println("Quantity InStock" + quantityInStock2);
                System.out.println("Buy Price: " + buyPrice2);
                System.out.println("");
            }
            conn.close();
        }
        public static void ShowCustomer() throws SQLException {
            Connection conn = DriverManager.getConnection(dbUrl, username, password);

            Statement statement3 = conn.createStatement();
            String sqlStatement3 = "SELECT customerNumber,customerName,contactLastName,contactFirstName,postalCode,city,state FROM customers";
            ResultSet resultSet3 = statement3.executeQuery(sqlStatement3);
            while(resultSet3.next()) {
                // Daten auslesen mittels Feldname
                int customerNumber = resultSet3.getInt("customerNumber");
                String customerName = resultSet3.getString("customerName");
                String contactLastName = resultSet3.getString("contactLastName");
                String contactFirstName = resultSet3.getString("contactFirstName");
                String postalCode = resultSet3.getString("postalCode");
                String city = resultSet3.getString("city");
                String state = resultSet3.getString("state");

                System.out.println("customerNumber: " + customerNumber);
                System.out.println("customerName:" + customerName);
                System.out.println("contactLastName: " + contactLastName);
                System.out.println("contactFirstName: " + contactFirstName);
                System.out.println("postalCode" + postalCode);
                System.out.println("city: " + city);
                System.out.println("state: " + state);
                System.out.println("");
            }
            conn.close();
        }
        public static void SearchCustomer() throws SQLException {
            Connection conn = DriverManager.getConnection(dbUrl, username, password);
            System.out.println("");
            Scanner myOb4j = new Scanner(System.in);
            System.out.println("customerName:");

            String myOb4jv = myOb4j.nextLine();  // Read user input
            String sqlStatement4 = "SELECT customerNumber,customerName,contactLastName,contactFirstName,postalCode,city,state FROM customers where customerName = ?";
            PreparedStatement prepStatemt2 = conn.prepareStatement(sqlStatement4);
            prepStatemt2.setString(1, myOb4jv);
            ResultSet d2 = prepStatemt2.executeQuery();
            while(d2.next()) {
                // Daten auslesen mittels Feldname
                int customerNumber = d2.getInt("customerNumber");
                String customerName = d2.getString("customerName");
                String contactLastName = d2.getString("contactLastName");
                String contactFirstName = d2.getString("contactFirstName");
                String postalCode = d2.getString("postalCode");
                String city = d2.getString("city");
                String state = d2.getString("state");

                System.out.println("customerNumber: " + customerNumber);
                System.out.println("customerName:" + customerName);
                System.out.println("contactLastName: " + contactLastName);
                System.out.println("contactFirstName: " + contactFirstName);
                System.out.println("postalCode" + postalCode);
                System.out.println("city: " + city);
                System.out.println("state: " + state);
                System.out.println("");

            }
            conn.close();
        }
        public static void AddProduct() throws SQLException {
            Connection conn = DriverManager.getConnection(dbUrl, username, password);

            Scanner productCode_Scanner = new Scanner(System.in);
            System.out.println("productCode:");
            String productCode = productCode_Scanner.nextLine();

            Scanner productName_Scanner = new Scanner(System.in);
            System.out.println("productName:");
            String productName = productName_Scanner.nextLine();

            Scanner productLine_Scanner = new Scanner(System.in);
            System.out.println("productLine:");
            String productLine = productLine_Scanner.nextLine();

            Scanner productVendor_Scanner = new Scanner(System.in);
            System.out.println("productVendor:");
            String productVendor = productVendor_Scanner.nextLine();

            Scanner quantityInStock_Scanner = new Scanner(System.in);
            System.out.println("quantityInStock:");
            short quantityInStock = Short.parseShort(quantityInStock_Scanner.nextLine());  // Read user input

            Scanner buyPrice_Scanner = new Scanner(System.in);
            System.out.println("buyPrice");
            double buyPrice = Double.parseDouble(buyPrice_Scanner.nextLine());  // Read user input

            Scanner productscale_Scanner = new Scanner(System.in);
            System.out.println("productscale");
            String productscale = productscale_Scanner.nextLine();  // Read user input

            Scanner MSRP_Scanner = new Scanner(System.in);
            System.out.println("MSRP");
            double MSRP = Double.parseDouble(MSRP_Scanner.nextLine());  // Read user input

            Scanner productDescription_Scanner = new Scanner(System.in);
            System.out.println("productDescription:");
            String productDescription = productDescription_Scanner.nextLine();  // Read user input

            String query = "INSERT INTO products (productCode, productName, productLine, productVendor,quantityInStock,buyPrice, productScale, MSRP,productDescription) VALUES(?, ?, ?, ?,?,?,?,?,?)";
            PreparedStatement prepStatemt5 = conn.prepareStatement(query);
            prepStatemt5.setString(1, productCode);
            prepStatemt5.setString(2, productName);
            prepStatemt5.setString(3, productLine);
            prepStatemt5.setString(4, productVendor);
            prepStatemt5.setShort(5, quantityInStock);
            prepStatemt5.setDouble(6, buyPrice);
            prepStatemt5.setString(7, productscale);
            prepStatemt5.setDouble(8, MSRP);
            prepStatemt5.setString(9, productDescription);

            prepStatemt5.executeUpdate();
            prepStatemt5.close();
            conn.close();
        }
        public static void ChangeProduct() throws SQLException {
            Connection conn = DriverManager.getConnection(dbUrl, username, password);

            System.out.println("");
            Scanner selectProduct = new Scanner(System.in);
            System.out.println("Product Code von dem Gewüschten Produkt:");
            String productcode = selectProduct.nextLine();  // Read user input

            Scanner descriptionscanner = new Scanner(System.in);
            System.out.println("New Description:");
            String description =  descriptionscanner.nextLine();  // Read user input
            System.out.println("");

            Scanner Productnamescanner = new Scanner(System.in);
            System.out.println("New Productname:");
            String Productname =  Productnamescanner.nextLine();  // Read user input

            String sqlStatement6 = "update products set productDescription = ? where productCode = ?";
            PreparedStatement prepStatemt6 = conn.prepareStatement(sqlStatement6);
            prepStatemt6.setString(1, description);

            prepStatemt6.setString(2, productcode);
            prepStatemt6.executeUpdate();


            String sqlStatement7 = "update products set productName = ? where productCode = ?";
            PreparedStatement prepStatemt7 = conn.prepareStatement(sqlStatement7);
            prepStatemt7.setString(1, Productname);
            prepStatemt7.setString(2, productcode);
            prepStatemt7.executeUpdate();
            conn.close();
        }
        public static void Deleteproduct() throws SQLException {
            Connection conn = DriverManager.getConnection(dbUrl, username, password);

            Scanner ProductCodedeletescanner = new Scanner(System.in);
            System.out.println("Enter Product Code to delete:");
            String ProductCode =  ProductCodedeletescanner.nextLine();  // Read user input

            String sqlStatement8 = "delete from products where productCode = ?";
            PreparedStatement prepStatemt8 = conn.prepareStatement(sqlStatement8);
            prepStatemt8.setString(1, ProductCode);
            prepStatemt8.executeUpdate();
            conn.close();

        }
}

