$solutionDirectory = (Get-Item -Path ".\" -Verbose).parent.FullName


function getBundleDirectories(){
	$directories = @()

	Get-ChildItem -Path $solutionDirectory -Filter *.sqlbundle -Recurse -File -Name| ForEach-Object {		
		$file = $_
		$file = "$solutionDirectory\$file"
		$directory = Split-Path $file -parent
		$directories += $directory
	}

	return $directories | select -uniq
};

function execute($path){
	Invoke-Expression "$env:SystemRoot\System32\cmd.exe /c $path"
};

function bundle($directory){
	cd $directory
	Get-ChildItem -Path $directory -Filter *.bat -Recurse -File -Name| ForEach-Object {		
		execute $_;
	};
};


function bundleAll($directories){
	foreach($directory in $directories){
		bundle $directory;
	};
};

$directories = getBundleDirectories;
bundleAll $directories;
