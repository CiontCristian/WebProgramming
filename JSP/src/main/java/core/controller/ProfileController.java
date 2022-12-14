package core.controller;

import core.domain.User;
import core.model.DBManager;
import org.apache.commons.lang3.math.NumberUtils;
import org.json.simple.JSONArray;
import org.json.simple.JSONObject;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;
import java.io.IOException;
import java.io.PrintWriter;
import java.util.ArrayList;

public class ProfileController extends HttpServlet {
    public ProfileController(){
        super();
    }

    protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        HttpSession session = request.getSession();
        User check = (User) session.getAttribute("user");
        if(check == null){
            return;
        }

        String action = request.getParameter("action");
        DBManager dbManager = new DBManager();
        PrintWriter printWriter = new PrintWriter(response.getOutputStream());
        if(action!=null && action.equals("change")){

            String email = request.getParameter("email_input");
            String name = request.getParameter("name_input");
            String password = request.getParameter("password_input");
            String address = request.getParameter("address_input");
            String picture = request.getParameter("picture_input");
            String hometown = request.getParameter("hometown_input");
            String age = request.getParameter("age_input");


            String prevEmail = DBManager.getPrevEmail();
            System.out.println(prevEmail);
            System.out.println(email);
            int userID = dbManager.findByEmail(prevEmail);

            if(userID != -1){
                dbManager.updateProfile(userID, new User(email, name, password, address, picture, hometown, Integer.parseInt(age)));
                dbManager.setPrevEmail(email);
                printWriter.println("Profile updated successfully!");
            }
            else{
                printWriter.println("Error updating profile!");
            }
            printWriter.flush();
        }
        else if(action != null && action.equals("get")){
            response.setContentType("application/json");
            String filter = request.getParameter("filter");
            ArrayList<User> users;
            if(NumberUtils.isCreatable(filter)){
                int filterInt = Integer.parseInt(filter);
                users = dbManager.getUsers(filterInt);
            }else{
                users = dbManager.getUsers(filter);
            }
            JSONArray usersJSON = new JSONArray();
            users.forEach(user -> {
                JSONObject userJSON = new JSONObject();
                userJSON.put("name", user.getName());
                userJSON.put("email", user.getEmail());
                userJSON.put("address", user.getAddress());
                userJSON.put("picture", user.getPicture());
                userJSON.put("hometown", user.getHometown());
                userJSON.put("age", user.getAge());
                usersJSON.add(userJSON);
            });

            //if(users.size() == 0){
            //    printWriter.write("No Matches!");
            //}
            //else {
            printWriter.println(usersJSON.toJSONString());
            //}
            printWriter.flush();
        }
        else if(action.equals("currentUser")){
            User user = DBManager.getCurrentUser();
            JSONArray usersJSON = new JSONArray();
            JSONObject userJSON = new JSONObject();
            userJSON.put("name", user.getName());
            userJSON.put("email", user.getEmail());
            userJSON.put("address", user.getAddress());
            userJSON.put("picture", user.getPicture());
            userJSON.put("hometown", user.getHometown());
            userJSON.put("age", user.getAge());
            usersJSON.add(userJSON);

            printWriter.println(usersJSON.toJSONString());
            printWriter.flush();
        }
    }
}
