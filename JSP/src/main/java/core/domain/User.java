package core.domain;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

@NoArgsConstructor
@AllArgsConstructor
@Data
public class User {
    private String email;
    private String name;
    private String password;
    private String address;
    private String picture;
    private String hometown;
    private int age;

}
