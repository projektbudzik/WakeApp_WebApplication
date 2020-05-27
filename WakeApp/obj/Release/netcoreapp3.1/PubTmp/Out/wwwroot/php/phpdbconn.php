<?php
class phpdbconn {
    private $conn;
 
    // Połącz z bazą
    public function connect() {
       
        // pobierz dane dla serwera z pliku phpdbconfig
        require_once 'phpdbconfig.php';
       
        // przypisz połączenie do zmiennej
   $this->conn = new mysqli(servername, mysql_user, mysql_pas, dbname);

        // zwróć połączenie

        return $this->conn;
    }
}
 
?>