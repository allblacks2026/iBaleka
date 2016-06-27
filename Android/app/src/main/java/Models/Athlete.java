package Models;

import java.util.Date;

<<<<<<< HEAD
/**
 * Created by Okuhle on 6/12/2016.
 */

//Based on the api on asp
public class Athlete {

    public int athleteID;
    public String firstName;
    public String surname;
    public double height;
    public double weight;
    public char gender;
    public String licenseNumber;
    public Date joinDate;
    public boolean deleted;

    public Athlete(int athleteID, boolean deleted, String firstName, char gender, double height, Date joinDate, String licenseNumber, String surname, double weight) {
        this.athleteID = athleteID;
        this.deleted = deleted;
        this.firstName = firstName;
        this.gender = gender;
        this.height = height;
        this.joinDate = joinDate;
        this.licenseNumber = licenseNumber;
        this.surname = surname;
        this.weight = weight;
    }

    public int getAthleteID() {
        return athleteID;
    }

    public void setAthleteID(int athleteID) {
        this.athleteID = athleteID;
    }

    public boolean isDeleted() {
        return deleted;
    }

    public void setDeleted(boolean deleted) {
        this.deleted = deleted;
    }

    public String getFirstName() {
        return firstName;
    }

    public void setFirstName(String firstName) {
        this.firstName = firstName;
    }

    public char getGender() {
        return gender;
    }

    public void setGender(char gender) {
        this.gender = gender;
    }

    public double getHeight() {
        return height;
    }

    public void setHeight(double height) {
        this.height = height;
    }

    public Date getJoinDate() {
        return joinDate;
    }

    public void setJoinDate(Date joinDate) {
        this.joinDate = joinDate;
    }

    public String getLicenseNumber() {
        return licenseNumber;
    }

    public void setLicenseNumber(String licenseNumber) {
        this.licenseNumber = licenseNumber;
    }

    public String getSurname() {
        return surname;
    }

    public void setSurname(String surname) {
        this.surname = surname;
    }

    public double getWeight() {
        return weight;
    }

    public void setWeight(double weight) {
        this.weight = weight;
    }
}
=======
public class Athlete {

    private String athleteID;
    private String userID;
    private String weight;
    private String height;
    private String gender;
    private String licenseNo;

    public Athlete(String athleteID, String userID, String weight, String height, String gender, String licenseNo) {
        this.athleteID = athleteID;
        this.userID = userID;
        this.weight = weight;
        this.height = height;
        this.gender = gender;
        this.licenseNo = licenseNo;
    }

    //Constructor excluding the license number

    public Athlete(String gender, String height, String weight, String userID, String athleteID) {
        this.gender = gender;
        this.height = height;
        this.weight = weight;
        this.userID = userID;
        this.athleteID = athleteID;
    }

    public String getAthleteID() {
        return athleteID;
    }

    public void setAthleteID(String athleteID) {
        this.athleteID = athleteID;
    }

    public String getUserID() {
        return userID;
    }

    public void setUserID(String userID) {
        this.userID = userID;
    }

    public String getWeight() {
        return weight;
    }

    public void setWeight(String weight) {
        this.weight = weight;
    }

    public String getHeight() {
        return height;
    }

    public void setHeight(String height) {
        this.height = height;
    }

    public String getGender() {
        return gender;
    }

    public void setGender(String gender) {
        this.gender = gender;
    }

    public String getLicenseNo() {
        return licenseNo;
    }

    public void setLicenseNo(String licenseNo) {
        this.licenseNo = licenseNo;
    }
}
>>>>>>> 8be16b6dcc6c546a4e3487ad4a1ee759e9d2a362
