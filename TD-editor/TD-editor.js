
//var jsondata = require("data.json");
//var data = JSON.parse(jsondata);

//console.log(data);
var canvas = document.getElementById("TD-editor");
var ctx = canvas.getContext("2d");

var dimensionX = data.dimensionX;
var dimensionY = data.dimensionY;

var tileCount = dimensionX * dimensionY;
var cellWidth = data.cellWidth;
var cellHeight = data.cellHeight;

let tileIcons;


//get dimension controls
var colsInput = document.getElementById("cols");
var rowsInput = document.getElementById("rows");
var cellWInput = document.getElementById("cellW");
var cellHInput = document.getElementById("cellH");

//dimension control input
colsInput.addEventListener("change",updateDimensions);
rowsInput.addEventListener("change", updateDimensions);
cellWInput.addEventListener("change", updateDimensions);
cellHInput.addEventListener("change", updateDimensions);

function updateDimensions(){
    ctx.clearRect(0,0,ctx.canvas.width,ctx.canvas.height);
    dimensionX = colsInput.value;
    dimensionY = rowsInput.value;
    cellWidth = cellWInput.value;
    cellHeight = cellHInput.value;
    tileCount = dimensionX * dimensionY;
    drawGrid(tileCount, dimensionX, cellWidth, cellHeight);
}


//click inside grid
ctx.canvas.addEventListener("click", (e)=>{
//    calculate tileindex from x y position
    var ypos = e.clientY - ctx.canvas.getBoundingClientRect().top;
    var xpos = e.clientX - ctx.canvas.getBoundingClientRect().left;

    if(xpos < dimensionX*cellWidth && ypos < dimensionY*cellHeight){
        var row = Math.floor(ypos/ cellHeight);
        var col = Math.floor(xpos/ cellWidth);
        console.log( row +"R :: C"+ col );
    
        var index = col + (row*dimensionX);
        console.log("index : "+index); 
    }else{
        console.log("out of bounds");
    }
    
});


//draw grid on start

if(loadTileIcons(5)){
    setTimeout(()=>{        
        drawGrid(tileCount, dimensionX, cellWidth, cellHeight)},500);           
}
function drawGrid(length, cols, cellW, cellH){
  
    for(var i = 0; i < length;i++){
        var x = cellW*(i%cols);
        var y = Math.floor(i/cols)*cellH;
        if(data.tiles[i]){
            printTileIcon(x,y,cellW,cellH, data.tiles[i]);
            printRectangle(x,y,cellW,cellH);
        }else{
            printRectangle(x,y,cellW,cellH);
        }       
    }
}
function printRectangle(x,y,w,h){    
    ctx.beginPath();
    ctx.lineWidth = 1;
    ctx.strokeStyle = "black";    
    ctx.rect(x,y,w,h);
    ctx.stroke();

}
function printTileIcon(x,y,w,h,tileData){
    var currentTileIcon;
    switch(tileData.state){
        case "blocked":
            currentTileIcon = tileIcons[0];
        break;
        case "path":
            currentTileIcon = tileIcons[1];
        break;
        case "free":
            currentTileIcon = tileIcons[2];
        break;
        case "spawn":
            currentTileIcon = tileIcons[3];
        break;
        case "waypoint":
            currentTileIcon = tileIcons[4];
        break;
    }
    ctx.drawImage(currentTileIcon,x,y,w,h);   
}
function loadTileIcons(imageCount){
    tileIcons = [imageCount];
    for (let i = 0; i < imageCount; i++) {
       tileIcons[i] = new Image();      
       tileIcons[i].src = "./img/"+(i+1)+".png";                      
    }
    return true;
}

