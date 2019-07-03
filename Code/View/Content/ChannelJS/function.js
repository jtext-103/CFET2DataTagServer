function load() {

    var path = $('#pathId').val();
    path = path.replace(/\//g, ".");
    path = path.replace(/\\/g, ".");

    var start = $('#startId').val();
    var end = $('#endId').val();
    var step = (end - start) / 1200;
    var length;

    //请求采样率
    $.ajax({
        url: thingPath + '/samplerate/' + path,
        type: "get",
        async: true,
        success: function (data) {
            var obj = JSON.parse(data);
            document.getElementById("sampleId").innerHTML = "  Sample Rate : " + obj.CFET2CORE_SAMPLE_VAL;
        }
    });

    //请求总长度
    $.ajax({
        url: thingPath + '/length/' + path,
        type: "get",
        async: false,
        success: function (data) {
            var obj = JSON.parse(data);
            document.getElementById("lengthId").innerHTML = "  Length : " + obj.CFET2CORE_SAMPLE_VAL;
            length = obj.CFET2CORE_SAMPLE_VAL;
        }
    });

    //判断输入格式并调整，或者提示输入错误
    //ForTest
    if (start >= end) {
        start = 0;
        end = length;
        step = (end - start) / 1200;
    }

    if (end > length) {
        end = length;
    }

    if (start < 0) {
        start = 0;
    }

    var apiLoad = thingPath + '/databystepd/' + path + '/'
        + start.toString() + '/' + end.toString() + '/' + step.toString();

    $.ajax({
        url: apiLoad,
        type: "get",
        async: true,
        success: function (data) {
            var obj = JSON.parse(data);
            var parsedData = parseData(obj);
            drawChart(parsedData, start, end);
        }
    });
}

function parseData(data) {
    var arrData = [];
    for (var i in data.CFET2CORE_SAMPLE_VAL) {
        arrData.push(data.CFET2CORE_SAMPLE_VAL[i]);
    }
    return arrData;
}

function drawChart(data, start, end) {

    //这里要删除所有d3元素重画
    d3.select(document.getElementById("svgId")).select('g').remove();

    var svgWidth = 1300, svgHeight = 400;
    var margin = { top: 20, right: 50, bottom: 30, left: 50 };
    var width = svgWidth - margin.left - margin.right;
    var height = svgHeight - margin.top - margin.bottom;

    //选中HTML中的svg元素
    var svg = d3.select(document.getElementById("svgId"))
        .attr("width", svgWidth)
        .attr("height", svgHeight);

    var svg = svg.append("g")
        .attr("transform", "translate(" + margin.left + "," + margin.top + ")");

    //缩放横轴
    var x = d3.scaleLinear()
        .rangeRound([0, width]);

    //缩放纵轴
    var y = d3.scaleLinear()
        .rangeRound([height, 0]);

    var line = d3.line()
        .x(function (d, i) { return x(i) })
        .y(function (d) { return y(d) })
    x.domain(d3.extent(data, function (d, i) { return i }));
    y.domain(d3.extent(data, function (d) { return d }));

    //画左边 x轴
    let x1 = d3.scaleLinear().range([0, width])
    let xScale = x1.domain([start, end])

    svg.append('g')
        .attr('class', 'xAxis')
        .attr("transform", "translate(0," + height + ")")
        .call(d3.axisBottom(xScale))

    //画左边 y 轴
    svg.append("g")
        .call(d3.axisLeft(y))

    //画内容曲线
    svg.append("path")
        .datum(data)
        .attr("fill", "none")
        .attr("stroke", "steelblue")
        .attr("stroke-linejoin", "round")
        .attr("stroke-linecap", "round")
        .attr("stroke-width", 1.5)
        .attr("d", line);
}
