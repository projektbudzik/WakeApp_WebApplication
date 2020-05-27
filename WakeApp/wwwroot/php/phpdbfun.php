<?php

class phpdbfun {
 
    private $conn;
 
    // kontruktor
	// Na początku utwórz połączenie bazodanowe zapisane w pliku phpdbconn.php
    function __construct() {
        require_once 'phpdbconn.php';
        // połącz z bazą
        $db = new phpdbconn();
        $this->conn = $db->connect();
    }
 
    //zarejestruj użytkownika
    public function regUser($name, $email, $password) {

        $hash = $this->passwordSHA($password);
        $encrypted_password = $hash["encrypted"]; // haslo
        $salt = $hash["salt"]; // sól
 
        $stmt = $this->conn->prepare("INSERT INTO `user` (`Name`, `Password`,`Salt`, `Email`) VALUES (?, ?, ?, ?)");
        $stmt->bind_param("ssss", $name, $encrypted_password, $salt, $email);
        $result = $stmt->execute();
        $stmt->close();

        if ($result) {
            $stmt = $this->conn->prepare("SELECT * FROM `user` WHERE `Name` = ?");
            $stmt->bind_param("s", $name);
            $stmt->execute();
            $user = $stmt->get_result()->fetch_assoc();
            $stmt->close();
 
            return $user;
        } else {
            return false;
        }
    }
 

    //zarejestruj grupe
    public function regGroup($name, $password) {

        $hash = $this->passwordSHA($password);
        $encrypted_password = $hash["encrypted"]; // haslo
        $salt = $hash["salt"]; // sól
 
        $stmt = $this->conn->prepare("INSERT INTO `group` (`Name`, `Password`,`Salt`) VALUES (?, ?, ?)");
        $stmt->bind_param("sss", $name, $encrypted_password, $salt);
        $result = $stmt->execute();
        $stmt->close();

        if ($result) {
            $stmt = $this->conn->prepare("SELECT * FROM `group` WHERE `Name` = ?");
            $stmt->bind_param("s", $name);
            $stmt->execute();
            $user = $stmt->get_result()->fetch_assoc();
            $stmt->close();
 
            return $user;
        } else {
            return false;
        }
    }
    //zaloguj użytkownika
    public function logUser($name, $password) {
 
        $stmt = $this->conn->prepare("SELECT * FROM `user` WHERE `Name` = ?");
        $stmt->bind_param("s", $name);
 
        if ($stmt->execute()) {
            $user = $stmt->get_result()->fetch_assoc();
            $stmt->close();
 
            // odszyfrowanie hasla
            $salt = $user['Salt'];
            $encrypted_password = $user['Password'];
            $hash = $this->checkPasswordSSHA($salt, $password);
            // porownanie hasel
            if ($encrypted_password == $hash) {
                // gdy autoryzacja pozytywna
                return $user;
            }
        } else {
            return NULL;
        }
    }
	
	  //zaloguj użytkownika
    public function logGroup($name, $password) {
 
        $stmt = $this->conn->prepare("SELECT * FROM `group` WHERE `Name` = ?");
        $stmt->bind_param("s", $name);
 
        if ($stmt->execute()) {
            $user = $stmt->get_result()->fetch_assoc();
            $stmt->close();
 
            // odszyfrowanie hasla
            $salt = $user['Salt'];
            $encrypted_password = $user['Password'];
            $hash = $this->checkPasswordSSHA($salt, $password);
            // porownanie hasel
            if ($encrypted_password == $hash) {
                // gdy autoryzacja pozytywna
                return $user;
            }
        } else {
            return NULL;
        }
    }

    //czy użytkownik istnieje
    public function checkUser($name) {
        $stmt = $this->conn->prepare("SELECT * from `user` WHERE `Name` = ?");

        $stmt->bind_param("s", $name);
        $stmt->execute();
        $stmt->store_result();
 
        if ($stmt->num_rows > 0) {
            // uzytkownik istnieje
            $stmt->close();
            return true;
        } else {
            //  uzytkownik nie istnieje
            $stmt->close();
            return false;
        }
    }
 
//czy grupa istnieje
    public function checkGroup($name) {
        $stmt = $this->conn->prepare("SELECT * from `group` WHERE `Name` = ?");

        $stmt->bind_param("s", $name);
        $stmt->execute();
        $stmt->store_result();
 
        if ($stmt->num_rows > 0) {
            // uzytkownik istnieje
            $stmt->close();
            return true;
        } else {
            //  uzytkownik nie istnieje
            $stmt->close();
            return false;
        }
    }
     

public function PasswordSHA($password) {
 
        $salt = sha1(rand());
        $salt = substr($salt, 0, 10); // sól - 10 losowych znaków
        $encrypted = base64_encode(sha1($password . $salt, true) . $salt);
        $hash = array("salt" => $salt, "encrypted" => $encrypted);
		
        return $hash;
    }
 
    public function checkPasswordSSHA($salt, $password) {
 
        $hash = base64_encode(sha1($password . $salt, true) . $salt);
 
        return $hash;
    }
 
}
 
?>