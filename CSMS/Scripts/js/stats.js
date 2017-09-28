jQuery(document).ready(function ($) {
    $("#menu").mmenu({
        "navbar": {
            "title": "统计"
        },
        "extensions": [
			"pagedim-black"
        ]
    });
});
$("#menu").removeClass("nav-hidden");

$("#sales").click(function () {
    $("html,body").animate({
        scrollTop: 0
    }, 500);
});

$("#salesBar").click(function () {
    $("html,body").animate({
        scrollTop: 340
    }, 500);
});

$("#produciton").click(function () {
    $("html,body").animate({
        scrollTop: 650
    }, 500);
});

$("#producitonBar").click(function () {
    $("html,body").animate({
        scrollTop: 960
    }, 500);
});

$("#warehouse").click(function () {
    $("html,body").animate({
        scrollTop: 1270
    }, 500);
});

$("#warehouseBar").click(function () {
    $("html,body").animate({
        scrollTop: 1580
    }, 500);
});

//$(".content .glyphicon").click(function(){
//	$("#filter").modal("toggle");
//});
function ToArray(Json) {
    var reg = new RegExp("&quot;", "g"); //创建正则RegExp对象     
    var data2 = JSON.parse(Json.replace(reg, '"'));
    return data2;
}


$(function () {
    initChart($("#salesChart")[0], "收款情况", "总金额：￥", "未收金额", "已收金额", NoTotalRevenue, TotalRevenue);
    initBarChart($("#salesBarChart")[0], "各合同收款情况", "未收金额", "已收金额", ToArray(ContractName), ToArray(NoAmountCollection), ToArray(SubAffirmIncomeAmount));
    initChart($("#producitonChart")[0], "生产情况", "需求量：", "未生产量", "已生产量", SumNoTotalProduct, SumTotalProduct);
    initBarChart($("#producitonBarChart")[0], "各合同生产情况", "未生产量", "已生产量", ToArray(ContractName),ToArray(NoTotalProduct), ToArray(TotalProduct));
    initChart($("#warehouseChart")[0], "发货情况", "库存量：" + SumReserves, "未发货数量", "已发货数量", SumNoShippedCount, SumShippedCount);
    initBarChart($("#warehouseBarChart")[0], "各合同发货情况", "未发货数量", "已发货数量", ToArray(ContractName), ToArray(NoShippedCount), ToArray(ShippedCount));
});

function initChart(element, titleText, titleSubtext, legendData1, legendData2, dataValue1, dataValue2) {
    var myChart = echarts.init(element);
    var _subtext = titleSubtext + (dataValue1 + dataValue2);
    if (titleText == "发货情况") {
        _subtext = titleSubtext;
    }
    var option = {
        title: {
            text: titleText,
            subtext: _subtext,
            x: 'center'
        },
        tooltip: {
            trigger: 'item',
            formatter: "{b} : ￥{c} ({d}%)"
        },
        legend: {
            orient: 'vertical',
            left: 'left',
            data: [legendData1, legendData2]
        },
        series: [{
            name: "contractIncome",
            type: 'pie',
            radius: '55%',
            center: ['50%', '60%'],
            data: [{
                value: dataValue1,
                name: legendData1
            },
				{
				    value: dataValue2,
				    name: legendData2
				}
            ],
            itemStyle: {
                emphasis: {
                    shadowBlur: 10,
                    shadowOffsetX: 0,
                    shadowColor: 'rgba(0, 0, 0, 0.5)'
                }
            }
        }]
    };
    myChart.setOption(option, false, true);
}

function initBarChart(element, titleText, legendData1, legendData2, aContractName, dataValue1, dataValue2) {
    var myBarChart = echarts.init(element);
    var option = {
        title: {
            text: titleText,
            x: 'center'
        },
        tooltip: {
            trigger: 'item',
            formatter: "{a} : ￥{c}"
        },
        legend: {
            orient: 'vertical',
            left: 'left',
            data: [legendData1, legendData2],
        },
        grid: {
            bottom: '30%'
        },
        xAxis: {
            data: aContractName,
            axisLabel: {
                interval: 0, //横轴信息全部显示  
                rotate: -30, //-30度角倾斜显示  
            }
        },
        yAxis: {},
        dataZoom: [{ // 这个dataZoom组件，默认控制x轴。
            type: 'slider', // 这个 dataZoom 组件是 slider 型 dataZoom 组件
            start: 0, // 左边在 10% 的位置。
            end: 10, // 右边在 60% 的位置。
            //				filterMode: 'empty'
        },
			{ // 这个dataZoom组件，也控制x轴。
			    type: 'inside', // 这个 dataZoom 组件是 inside 型 dataZoom 组件
			    start: 0, // 左边在 10% 的位置。
			    end: 10 // 右边在 60% 的位置。
			}
        ],
        series: [{
            name: legendData2,
            type: 'bar',
            stack: 'barChart',
            data: dataValue1,
            itemStyle: {
                normal: {
                    color: '#333'
                }
            }
        },
			{
			    name: legendData1,
			    type: 'bar',
			    stack: 'barChart',
			    data: dataValue2
			}
        ]
    };
    myBarChart.setOption(option, false, true);
}