const connection = new signalR.HubConnectionBuilder()
    .withUrl("http://localhost:5250/gameHub") // Ajusta la URL
    .configureLogging(signalR.LogLevel.Information)
    .build();

// Elementos del DOM
const gridContainer = document.getElementById("grid-container");
const yearElement = document.getElementById("year");
const creatureCountElement = document.getElementById("creatureCount");
const startBtn = document.getElementById("startBtn");

// Conectar al Hub
async function startConnection() {
    try {
        await connection.start();
        console.log("Conectado a SignalR");
        startBtn.disabled = false;
    } catch (err) {
        console.error(err);
        setTimeout(startConnection, 5000); // Reconectar en 5 segundos
    }
}

// Escuchar actualizaciones del servidor
connection.on("ReceiveUpdate", (snapshot) => {
    console.log(snapshot);
    renderGrid(snapshot);
    updateStats(snapshot.yearTime, snapshot.creaturesPositions.length);
});

// Renderizar el grid
function renderGrid(gridData) {
    console.log(gridData);
    
    gridContainer.innerHTML = '';
    gridContainer.style.gridTemplateColumns = `repeat(${gridData.world.width}, 20px)`;

    // for (const clave in gridData.world.creaturePositions) {
    //     if (creaturePositions.hasOwnProperty(clave)) {
    //         const valor = creaturePositions[clave];
    //         console.log(valor);
    //         if (typeof valor === "string") {
    //             console.log(`Clave: ${clave}, Valor (string): ${valor}`);
    //         }
    //     }
    // }

    for (let y = 0; y < gridData.world.height; y++) {
        for (let x = 0; x < gridData.world.width; x++) {
            const cell = document.createElement("div");
            cell.className = "cell";
            
            // 1. Construir la clave como string "x,y"
            const clave = `${x},${y}`;

            // 2. Verificar si existe y tiene un valor no vacío
            if (
                gridData.world.creaturePositions.hasOwnProperty(clave) &&
                typeof gridData.world.creaturePositions[clave] === "string" &&
                gridData.world.creaturePositions[clave].trim() !== ""
            ) {
                cell.classList.add("filled");
                cell.textContent = "*";
            }
            gridContainer.appendChild(cell);
        }
    }
}

// Actualizar estadísticas
function updateStats(year, creatureCount) {
    yearElement.textContent = year;
    creatureCountElement.textContent = creatureCount;
}

// Iniciar simulación
startBtn.addEventListener("click", async () => {
    try {
        startBtn.textContent = "Simulación en curso...";
        startBtn.disabled = true;
    } catch (err) {
        console.error("Error al iniciar:", err);
    }
});

// Iniciar conexión al cargar la página
startConnection();