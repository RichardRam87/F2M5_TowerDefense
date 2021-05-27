var toolbarCanvas = document.getElementById("TD-toolbar");
var toolCtx = toolbarCanvas.getContext("2d");

toolCtx.font = "20px calibri";
toolCtx.fillText("Brush Selector",10,20);

var brush = "none";

var brushTypes = [
    "blocked",
    "path",
    "free",
    "spawn",
    "waypoint"
];
//make brush selector

var img = [5];
for(var i=0;i<5;i++){
    img[i] = new Image();        
    img[i].src = "./img/"+(i+1)+".png";
    
}
setTimeout(() => {           
    for(var i=0;i<img.length;i++){
        toolCtx.drawImage(img[i],10+(i*25),25,25,25);     
    }       
},10); 

toolCtx.toolbarCanvas.addEventListener("click", (e)=>{

    
})




