<?php

require_once 'phpdbfun.php';
$db = new phpdbfun();
 
// tablica json
$response = array("error" => FALSE);
 
if($_SERVER['REQUEST_METHOD']=='POST'){
 
    // receiving the post params
    $name = $_POST['name'];
    $password = $_POST['pass'];
 
    // get the user by email and password
    $user = $db->logGroup($name, $password);

    if ($user != false) {
        // użytkownik jest poprawny
        $response["error"] = FALSE;
        $response["status"] = "OK";
        $response["msg"] = "Logowanie pomyslne";
        $response["user"]["name"] = $user["Name"];

        echo json_encode($response);
    } else {
        // Użytkownik nie został znaleziony
        $response["error"] = TRUE;

        $response["error_msg"] = "Niepoprawny login lub hasło";
        echo json_encode($response);
    }
} else {
    // required post params is missing
    $response["error"] = TRUE;
    $response["error_msg"] = "Blad podczas przetwarzania metody POST";
    echo json_encode($response);
}
?>