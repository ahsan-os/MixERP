var scrudjson;
var kanbans;
var scrudForm = $(".form.factory");
var scrudView = $(".view.factory");
var addNewButton = $("#AddNewButton");
var viewReady = false;
var annotationLoaded = false;
var metaDefinition;
var falgDefinition;
var localizedHeaders = [];
var columns = [];
var columnSelect = $("#ColumnSelect");
var filterConditionSelect = $("#FilterConditionSelect");
var valueInputText = $("#ValueInputText");
var andInputText = $("#AndInputText");
var commandItems = "<a onclick='deleteFilter(this);'><i class='delete icon'></i></a><a onclick='editFilter(this);'><i class='edit icon'></i></a><a onclick='$(this).parent().parent().toggleClass(\"warning\");'><i class='check mark icon'></i></a>";
var filterButton = $("#FilterButton");
var updateButton = $("#UpdateButton");
var verificationPopUnder = $("#VerificationPopUnder");
var reasonTextArea = $("#ReasonTextArea");
var attachmentServiceUrl = "/api/forms/config/attachments/document/300/250/";


var filterConditions = [{ "value": "0", "text": "IsEqualTo", "operator": "=" },
{ "value": "1", "text": "IsNotEqualTo", "operator" :"!=" },
{ "value": "2", "text": "IsLessThan", "operator" : "<" },
{ "value": "3", "text": "IsLessThanEqualTo", "operator": "<=" },
{ "value": "4", "text": "IsGreaterThan", "operator": ">" },
{ "value": "5", "text": "IsGreaterThanEqualTo", "operator": ">=" },
{ "value": "6", "text": "IsBetween", "operator": "between" },
{ "value": "7", "text": "IsNotBetween", "operator": "not between" },
{ "value": "8", "text": "IsLike", "operator": "like" },
{ "value": "9", "text": "IsNotLike", "operator": "not like" }
];

if (!scrudFactory.card) {
    scrudFactory.card = new Object();
};
