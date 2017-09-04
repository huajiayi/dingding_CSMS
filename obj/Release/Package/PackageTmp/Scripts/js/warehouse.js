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

function insertAfter(newElement, targetElement) {
	var parent = targetElement.parentNode;
	if(parent.lastChild == targetElement) {
		parent.appendChild(newElement);
	} else {
		parent.insertBefore(newElement, targetElement.nextSibling);
	}
}

addLoadEvent(function () { fullData(ContractName, Count, Reserves, ShippedCount, NoShippedCount) });
addLoadEvent(function() {
    fullLog(WarehouseLogJson)
});

//填充数据
function fullData(contractName, demand, reserves, shippedCount, noShippedCount){
	document.getElementById("lbl_contractName").innerHTML = "合同名称：" + contractName;
	document.getElementById("lbl_demand").innerHTML = "需求量：" + demand;
	document.getElementById("lbl_reserves").innerHTML = "库存量：" + reserves;
	document.getElementById("lbl_shippedCount").innerHTML = "已发货数量：" + shippedCount;
	document.getElementById("lbl_noShippedCount").innerHTML = "未发货数量：" + noShippedCount;
}

//填充日志
function fullLog(WarehouseLogJson) {
	var log = document.getElementById("log");
	if (WarehouseLogJson == "[]") {
		log.innerHTML = "尚无日志";
	} else {
		var logList = document.createElement("section");
		logList.id = "cd-timeline";
		logList.className = "cd-container";
		insertAfter(logList, log);
		var reg = new RegExp("&quot;", "g"); //创建正则RegExp对象    
		var WarehouseLog = JSON.parse(WarehouseLogJson.replace(reg, '"'));
		for (var i = 0; i < WarehouseLog.length; i++) {
		    addLog(WarehouseLog[i].LogName, WarehouseLog[i].Shipments, WarehouseLog[i].ShippedDate, WarehouseLog[i].LogDate, WarehouseLog[i].Name);
		}
	}
}

//添加日志
function addLog(logName, log, ShippedDate, logDate, name) {
	var logList = document.getElementById("cd-timeline");
	var div_block = document.createElement("div");
	div_block.className = "cd-timeline-block";
	var div_img = document.createElement("div");
	div_img.className = "cd-timeline-img cd-picture";
	var img = document.createElement("img");
	img.src = "../../Content/img/cd-icon-picture.svg";
	div_img.appendChild(img);
	div_block.appendChild(div_img);
	var div_content = document.createElement("div");
	div_content.className = "cd-timeline-content";
	var h2 = document.createElement("h2");
	h2.innerHTML = logName;
	var h3 = document.createElement("h3");
	h3.innerHTML = "发货 " + log + " 套";
	var h3_date = document.createElement("h3");
	h3_date.innerHTML = "发货日期：<br/>" + ShippedDate;
	var span = document.createElement("span");
	span.className = "cd-date";
	span.innerHTML = logDate + " by " + name;
	div_content.appendChild(h2);
	div_content.appendChild(h3);
	div_content.appendChild(h3_date);
	div_content.appendChild(span);
	div_block.appendChild(div_content);
	logList.appendChild(div_block);
}