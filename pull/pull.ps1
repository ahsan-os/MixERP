$currentDirectory = (Get-Item -Path ".\" -Verbose).FullName
$pathToRoot = (Get-Item -Path ".\" -Verbose).parent.FullName
$pathToAreas = "$pathToRoot\src\Frapid.Web\Areas";

function getGitDirectory(){
	$gitDirectory = Get-Content "$currentDirectory\env\GitDirectory.txt";
	
	return $gitDirectory;
};


function gitPull($path, $projectName){
	cd $path;
	$path;
	
	"Reverting changes to $projectName";
	git.exe checkout .

	"Cleaning up $projectName";
	git clean -n
	git clean -fd

	"Pulling $projectName";	
	git.exe pull origin master
	"`n"
};

cd $pathToRoot

$gitDirectory = getGitDirectory;
$env:Path = $gitDirectory;
$projectName = "Frapid";

gitPull $pathToRoot $projectName;

cd $pathToAreas
$areas = Get-ChildItem -dir

foreach($area in $areas){
	$areaName = $area.Name;
	$pathToProject = "$pathToAreas\$areaName";
	gitPull $pathToProject $areaName;
};

cd
