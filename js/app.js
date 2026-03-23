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
  // stub — implemented in issue #3
  const status  = document.getElementById("status-text");
  const spinner = document.getElementById("spinner");

  spinner.style.display = "inline-block";
  status.textContent = `Loading: ${file.name}`;

  // Yield to browser to render spinner before synchronous work
  setTimeout(() => {
    spinner.style.display = "none";
    status.textContent = `Loaded: ${file.name}`;
    console.info("handleFile stub — not yet implemented", file.name);
  }, 0);
}
