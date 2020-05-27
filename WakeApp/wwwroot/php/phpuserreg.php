<?php

require_once 'phpdbfun.php';
$db = new phpdbfun();

// tablica json
$response = array("error" => FALSE);

if($_SERVER['REQUEST_METHOD']=='POST'){

	$username=$_POST['username'];
	$pass=$_POST['pass'];
	$email =$_POST['email'];
	
	if ($db->checkUser($username)) {
		// czy uzytkownik istnieje
		$response["error"] = TRUE;
        $response["status"] = "ERROR";
		$response["error_msg"] = "Uzytkownik '" .$username."' juz istnieje";
		echo json_encode($response);
	}else{
		$user = $db->regUser($username, $email, $pass);
		if ($user) {
            // uzytkownik utworzony
            $response["error"] = FALSE;
            $response["status"] = "Gotowe";
			$response["msg"] = "Uzytkownik utworzony";
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