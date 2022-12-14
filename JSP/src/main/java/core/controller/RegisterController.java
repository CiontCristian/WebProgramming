package core.controller;

import core.domain.User;
import core.model.DBManager;

import javax.servlet.RequestDispatcher;
import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;

public class RegisterController extends HttpServlet {
    public RegisterController(){
        super();
    }

    protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        String email = request.getParameter("email");
        String name = request.getParameter("name");
        String password = request.getParameter("passwordd");
        String address = request.getParameter("address");
        String picture = request.getParameter("picture");
        String hometown = request.getParameter("hometown");
        String age = request.getParameter("age");

        RequestDispatcher requestDispatcher = null;

        if(email.equals("") || name.equals("") || password.equals("") || address.equals("")
        || picture.equals("") || hometown.equals("") || Integer.parseInt(age) < 0){
            requestDispatcher = request.getRequestDispatcher("/error.jsp");
        }
        else{
            DBManager dbManager = new DBManager();
            boolean result = dbManager.register(new User(email, name, password, address, picture, hometown, Integer.parseInt(age)));
            if(result){
                requestDispatcher = request.getRequestDispatcher("/login.html");
            }
            else{
                requestDispatcher = request.getRequestDispatcher("/error.jsp");
            }
        }

        requestDispatcher.forward(request, response);

    }

}
