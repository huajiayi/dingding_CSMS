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

//返回主页面
function back(){
	var btn_save = document.getElementById("btn_save");
	btn_save.onclick = function(){
		location.href = "index.html";
	};
}
