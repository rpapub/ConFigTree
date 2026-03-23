const config = {
  namespace:      "Cpmf.Config",
  rootClassName:  "AppConfig",
  dotnetVersion:  "net6",
  xmlDocComments: true,
};

document.addEventListener("DOMContentLoaded", () => {
  if (typeof XLSX === "undefined") {
    console.error("SheetJS failed to load.");
  } else {
    console.info(`SheetJS loaded: ${XLSX.version}`);
  }

  const dropZone = document.getElementById("drop-zone");
  const fileInput = document.getElementById("file-input");
  const status    = document.getElementById("status-text");

  dropZone.addEventListener("click", () => fileInput.click());

  dropZone.addEventListener("dragover", (e) => {
    e.preventDefault();
    dropZone.classList.add("dragover");
  });

  dropZone.addEventListener("dragleave", () => {
    dropZone.classList.remove("dragover");
  });

  dropZone.addEventListener("drop", (e) => {
    e.preventDefault();
    dropZone.classList.remove("dragover");
    const file = e.dataTransfer.files[0];
    if (file) handleFile(file);
  });

  fileInput.addEventListener("change", () => {
    if (fileInput.files[0]) handleFile(fileInput.files[0]);
  });

  document.getElementById("copy-btn").addEventListener("click", () => {
    const text = document.getElementById("output").textContent;
    if (!text) return;
    navigator.clipboard.writeText(text).then(() => {
      status.textContent = "Copied to clipboard.";
      setTimeout(() => { status.textContent = ""; }, 2000);
    });
  });
});

function handleFile(file) {
  const status  = document.getElementById("status-text");
  const spinner = document.getElementById("spinner");

  spinner.style.display = "inline-block";
  status.textContent = `Loading: ${file.name}`;

  const reader = new FileReader();

  reader.onload = (e) => {
    try {
      const workbook = XLSX.read(e.target.result, { type: "array", cellDates: true });
      spinner.style.display = "none";
      status.textContent = `Loaded: ${file.name} (${workbook.SheetNames.length} sheets: ${workbook.SheetNames.join(", ")})`;
      console.info("Workbook loaded:", workbook.SheetNames);
      onWorkbookLoaded(workbook);
    } catch (err) {
      spinner.style.display = "none";
      status.textContent = `Error: ${err.message}`;
      console.error("Failed to parse workbook:", err);
    }
  };

  reader.onerror = () => {
    spinner.style.display = "none";
    status.textContent = "Error: could not read file.";
  };

  reader.readAsArrayBuffer(file);
}

function onWorkbookLoaded(workbook) {
  // stub — sheet mapping implemented in issue #4
  console.info("onWorkbookLoaded stub", workbook.SheetNames);
}
