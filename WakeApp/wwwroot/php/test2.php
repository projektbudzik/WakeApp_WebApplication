<?php 
try{
$soapclient = new SoapClient('/MESServices/MESConfirmOperation.asmx');
$param=array('userId'=>'12');
$response =$soapclient->ValidateUser($param);
var_dump($response);
echo '<br><br><br>';
$array = json_decode(json_encode($response), true);
print_r($array);
 echo '<br><br><br>';
	  echo '<br><br><br>';
	foreach($array as $item) {
		echo '<pre>'; var_dump($item);
	}  
}catch(Exception $e){
	echo $e->getMessage();
}	
?>