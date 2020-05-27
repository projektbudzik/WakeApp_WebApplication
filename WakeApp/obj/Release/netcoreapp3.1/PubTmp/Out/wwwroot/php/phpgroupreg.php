<?php

require_once 'phpdbfun.php';
$db = new phpdbfun();

// tablica json
$response = array("error" => FALSE);

if($_SERVER['REQUEST_METHOD']=='POST'){

	$username=$_POST['name'];
	$pass=$_POST['pass'];
	
	if ($db->checkGroup($username)) {
		// czy uzytkownik istnieje
		$response["error"] = TRUE;
        $response["status"] = "ERROR";
		$response["error_msg"] = "Grupa '" .$username."' juz istnieje";
		echo json_encode($response);
	}else{
		$user = $db->regGroup($username, $pass);
		if ($user) {
            // uzytkownik utworzony
            $response["error"] = FALSE;
            $response["status"] = "Gotowe";
	        $response["msg"] = "Grupa utworzona";
            $response["user"]["name"] = $user["name"];
            echo json_encode($response);
        } else {
            // user failed to store
            $response["error"] = TRUE;
            $response["error_msg"] = "Blad systemowy podczas rejestracji";
            echo json_encode($response);
        }
	}
}else{
	$response["error"] = TRUE;
    $response["error_msg"] = "Blad metody POST";
    echo json_encode($response);
}	
?>