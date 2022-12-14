package core.model;

import core.domain.User;

import java.sql.*;
import java.util.ArrayList;

public class DBManager {
    private Statement statement;
    private static String prevEmail;
    private static User currentUser;

    public DBManager(){
        connect();
    }

    public static String getPrevEmail() {
        return prevEmail;
    }

    public static User getCurrentUser() {
        return currentUser;
    }

    public void setPrevEmail(String _prevEmail) {
        prevEmail = _prevEmail;
    }

    public void connect(){
        try{
            Class.forName("org.postgresql.Driver");
            Connection connection=DriverManager.getConnection("jdbc:postgresql://localhost:5432/web", "postgres", "1234");
            statement = connection.createStatement();
        } catch (ClassNotFoundException | SQLException e) {
            System.out.println("Connection error: "+ e.getMessage());
            e.printStackTrace();
        }
    }

    public User authenticate(String email, String password){
        ResultSet resultSet;
        User user = null;
        System.out.println(email+" "+password);
        try{
            resultSet = statement.executeQuery("select * from users where email='"+email+"' and password='"+password+"'");
            if(resultSet.next()){
                user = new User(resultSet.getString("email"), resultSet.getString("name"), resultSet.getString("password"),
                        resultSet.getString("address"), resultSet.getString("picture"),
                        resultSet.getString("hometown"), resultSet.getInt("age"));
                prevEmail=user.getEmail();
                currentUser = user;
            }
            resultSet.close();

        } catch (SQLException throwables) {
            throwables.printStackTrace();
        }

        return user;
    }

    public boolean register(User user){
        int affected_rows = 0;
        try{
            affected_rows = statement.executeUpdate(
                    "insert into users(name, email, password, address, picture, hometown, age)" +
                            "values ('"+user.getName()+"','"+user.getEmail()+"','"+user.getPassword()+"'" +
                            ",'"+user.getAddress()+"','"+user.getPicture()+"','"+user.getHometown()+"'" +
                            ",'"+user.getAge()+"')");
        } catch (SQLException throwables) {
            throwables.printStackTrace();
        }
        return affected_rows > 0;
    }

    public boolean updateProfile(int id, User user){
        int affected_rows = 0;
        try{
            affected_rows = statement.executeUpdate(
                "update users set name='"+user.getName()+"', email='"+user.getEmail()+"'," +
                        "password='"+user.getPassword()+"',address='"+user.getAddress()+"'," +
                        "picture='"+user.getPicture()+"',hometown='"+user.getHometown()+"'," +
                        "age='"+user.getAge()+"' where id="+id
            );
        } catch (Exception e) {
            e.printStackTrace();
        }
        return affected_rows > 0;
    }

    public int findByEmail(String email){
        ResultSet resultSet;
        int userID = -1;
        try{
            resultSet = statement.executeQuery("select * from users where email='"+email+"'");
            if(resultSet.next()){
                userID = resultSet.getInt("id");
            }
            resultSet.close();
        } catch (SQLException throwables) {
            throwables.printStackTrace();
        }

        return userID;
    }

    public ArrayList<User> getUsers(Object match){
        ArrayList<User> users = new ArrayList<>();
        ResultSet resultSet = null;
        try{
            if(match instanceof String){
                String match2="%"+match+"%";
                resultSet = statement.executeQuery("select * from users where name LIKE '"+match2+"'" +
                        "or email LIKE '"+match2+"' or address LIKE '"+match2+"' or picture LIKE '"+match2+"'" +
                        "or hometown LIKE '"+match2+"'");
            }
            else if (match instanceof Integer){
                resultSet = statement.executeQuery("select * from users where age='"+match+"'");
            }
            while (resultSet.next()){
                users.add(new User(resultSet.getString("name"), resultSet.getString("email"),
                        resultSet.getString("password"), resultSet.getString("address"),
                        resultSet.getString("picture"), resultSet.getString("hometown"),
                        resultSet.getInt("age")));
            }
            resultSet.close();
        } catch (SQLException throwables) {
            throwables.printStackTrace();
        }

        return users;
    }
}

