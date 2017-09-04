

window.onload = function () {
    var reg = new RegExp("&quot;", "g"); //创建正则RegExp对象    
    var data = JSON.parse(arr.replace(reg, '"'));

    for (var i = 0; i < data.length; i++) {
        var listItem = document.createElement("a");
        listItem.href = "#";
        listItem.className = "list-group-item";
        listItem.innerHTML = data[i];
        contractListItems.appendChild(listItem);
    }


}

addLoadEvent(addClickEvent);
addLoadEvent(function(){updateContractList(contractArr)});

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

//为合同两侧按钮添加事件
function addClickEvent(){
	var contractList = document.getElementById("contractList");
	var btn_addContract = document.getElementsByTagName("span")[0];
	btn_addContract.onclick = function(){
	    location.href = "/MM/Index";
	};
	var btn_refresh = contractList.getElementsByTagName("span")[1];
	btn_refresh.onclick = test;
}

//添加假数据
function test(){
	var contractArr = new Array();
	for (var i = 1; i <= data.length; i++) {
	contractArr.push(data[i]);
}
	updateContractList(contractArr);
}

//往list添加数据
function updateContractList(contractArr){
	var contractListItems = document.getElementById("contractListItems");
	var listItems = contractListItems.childNodes;
	//先删除list内所有项
	for(var i = listItems.length - 1; i >= 0; i--){
		contractListItems.removeChild(listItems[i]);
	}
	//添加项
	for(var i = 0; i < contractArr.length; i++){
		var listItem = document.createElement("a");
		listItem.href = "#";
		listItem.className = "list-group-item";
		listItem.innerHTML = contractArr[i];
		contractListItems.appendChild(listItem);
	}
}
