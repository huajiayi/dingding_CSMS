

window.onload = function () {
    var reg = new RegExp("&quot;", "g"); //创建正则RegExp对象    
    var data = JSON.parse(arr.replace(reg, '"'));
    var reg = new RegExp("&quot;", "g"); //创建正则RegExp对象    
    var data2 = JSON.parse(arrID.replace(reg, '"'));
    for (var i = 0; i < data.length; i++) {
        var listItem = document.createElement("a");
       
        listItem.className = "list-group-item";
        listItem.innerHTML = data[i];
        contractListItems.appendChild(listItem);

<<<<<<< HEAD
        listItem.href = "/ContractandSales/ContractContent?ID=" + data2[i];
=======
        listItem.href = "/MM/ContractContent?ID="+data2[i];
>>>>>>> a37e0aa940cb2c06b1c21f1ea51099fc603735c9
    }
  

    $("#txt").val = "222";

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

function insertafter(newelement, targetelement) {
	var parent = targetelement.parentnode;
	if(parent.lastchild == targetelement) {
		parent.appendchild(newelement);
	} else {
		parent.insertbefore(newelement, targetelement.nextsibling);
	}
}

//为合同两侧按钮添加事件
function addClickEvent(){
	var contractList = document.getElementById("contractList");
	var btn_addContract = document.getElementsByTagName("span")[0];
	btn_addContract.onclick = function(){
<<<<<<< HEAD
	    location.href = "/ContractandSales/Index";
=======
	    location.href = "/MM/Index";
>>>>>>> a37e0aa940cb2c06b1c21f1ea51099fc603735c9
	};
	var btn_refresh = contractList.getElementsByTagName("span")[1];
	btn_refresh.onclick = test;
}

//添加假数据
function test(){
<<<<<<< HEAD
    location.href = "/ContractandSales/Login";
=======
    location.href = "/MM/Login";
>>>>>>> a37e0aa940cb2c06b1c21f1ea51099fc603735c9
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
