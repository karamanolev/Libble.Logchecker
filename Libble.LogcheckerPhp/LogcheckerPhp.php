<?
namespace Libble;

[\Export]
class LogcheckerPhp {
	private $logchecker;
	private $data;

	public function __construct($source) {
		$this->logchecker = new \LOG_CHECKER();
		$this->logchecker->new_file($source);
		$this->data = $this->logchecker->parse();
	}

	public function GetScore() {
		return $this->data[0];
	}
	
	public function GetGood() {
		return $this->data[1];
	}
	
	public function GetBad() {
		return $this->data[2];
	}
	
	public function GetLog() {
		return $this->data[3];
	}
}
?>