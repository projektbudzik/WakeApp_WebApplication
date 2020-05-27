<?php
$servername= "mysql3.webio.pl";
$mysql_user="19697_adm";
$mysql_pass="budzik1!";
$dbname="19697_budzik";

$conn = mysqli_connect($servername, $mysql_user , $mysql_pass, $dbname);

if($conn){
	echo("connection success");
}else{
	echo("connection not success");
}
?>