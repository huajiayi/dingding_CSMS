function addLoadEvent(func) {
	var oldonload = window.onload;
	if(typeof window.onload != 'function') {
		window.onload = func;
	} else {
		window.onload = function() {
			oldonload();
			func();
		}
	}
}

	


//addLoadEvent(back);
var serviceIndex = 0;


//添加一条服务款项
function addService() {
	var services = document.getElementById("services");
	var service = document.createElement("div");
	service.className = "input-group";
	service.style.marginTop = "0.5em";
	var textInput = document.createElement("input");
	textInput.type = "text";
	textInput.className = "form-control";
	serviceIndex++;
	textInput.id = "txt_service" + serviceIndex;
	textInput.name = "txt_service" + serviceIndex;
	textInput.placeholder = "请输入服务款项";
	textInput.required = "required";
	var label = document.createElement("label");
	label.className = "input-group-addon";
	label.style.backgroundColor = "red";
	label.style.color = "white";
	label.style.cursor = "pointer";
	label.onclick = function(){
		serviceIndex--;
		services.removeChild(service);
		$("#txt").val(serviceIndex);
	};
	var span = document.createElement("span");
	span.className = "glyphicon glyphicon-minus";
	label.appendChild(span);
	service.appendChild(textInput);
	service.appendChild(label);
	services.appendChild(service);
	$("#txt").val(serviceIndex);
}
function back() {
    window.history.back(-1);
    //if (sessionStorage.getItem("txt_keyword") == null) {
    //    var $txt_keyword = "";
    //} else {
    //    var $txt_keyword = sessionStorage.getItem("txt_keyword");
    //} if (sessionStorage.getItem("txt_startDate") == null && sessionStorage.getItem("txt_endDate") == null) {
    //    var $txt_startDate = "";
    //    var $txt_endDate = "";
    //} else {
    //    var $txt_startDate = sessionStorage.getItem("txt_startDate");
    //    var $txt_endDate = sessionStorage.getItem("txt_endDate");
    //}
    //location.href = "/Contract/filtration?txt_keyword=" + $txt_keyword + "&txt_startDate=" + $txt_startDate + "&txt_endDate=" + $txt_endDate;
}
//返回主页面
function back(){
	var btn_save = document.getElementById("btn_save");
	btn_save.onclick = function(){
		location.href = "index.html";
	};
}
