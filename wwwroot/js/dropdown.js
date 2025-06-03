document.addEventListener("DOMContentLoaded", () => {
    console.log("Dropdown script is running!");

    // === Project Dropdown Toggle ===
    document.querySelectorAll(".dropdown-toggle").forEach(toggle => {
        toggle.addEventListener("click", (e) => {
            e.stopPropagation();
            const parent = toggle.closest(".project-actions");
            document.querySelectorAll(".project-actions").forEach(el => el.classList.remove("open"));
            parent.classList.toggle("open");
        });
    });

    document.addEventListener("click", () => {
        document.querySelectorAll(".project-actions").forEach(el => el.classList.remove("open"));
    });

    // === Edit Project Modal ===
    const editModal = document.getElementById("editProjectModal");
    const editTitle = document.getElementById("editProjectTitle");
    const editDesc = document.getElementById("editProjectDesc");
    const editTime = document.getElementById("editProjectTime");
    const editImage = document.getElementById("editProjectImage");
    const editImageInput = document.getElementById("editProjectImageInput");
    const editIcon = document.querySelector(".edit-icon");
    const closeEditBtn = document.getElementById("closeEditModal");
    const saveEditBtn = document.getElementById("saveEditBtn");

    let selectedProject = null;

    document.querySelectorAll(".dropdown-menu .edit").forEach(editBtn => {
        editBtn.addEventListener("click", function (e) {
            e.preventDefault();
            document.querySelectorAll(".project-actions").forEach(el => el.classList.remove("open"));

            selectedProject = this.closest(".project");
            if (!selectedProject) {
                console.error("Project not found!");
                return;
            }

            const title = selectedProject.querySelector("h4")?.innerText.trim() || "";
            const desc = selectedProject.querySelector(".desc")?.innerText.trim() || "";
            const time = selectedProject.querySelector(".budget-wrapper input")?.value.trim() || "1000";
            const imageEl = selectedProject.querySelector(".preview-image");
            const imageSrc = imageEl && imageEl.src ? imageEl.src : "default-image.jpg";

            console.log("Editing Project:", title, desc, time, imageSrc);

            editTitle.value = title;
            editDesc.value = desc;
            editTime.value = time;
            editImage.src = imageSrc;
            editImage.style.display = imageEl ? "block" : "none";

            editModal.style.display = "flex";
        });
    });

    editIcon?.addEventListener("click", () => {
        editImageInput.click();
    });

    editImageInput?.addEventListener("change", function () {
        const file = this.files[0];
        if (file) {
            const reader = new FileReader();
            reader.onload = function (e) {
                editImage.src = e.target.result;
            };
            reader.readAsDataURL(file);
        }
    });

    closeEditBtn?.addEventListener("click", () => {
        editModal.style.display = "none";
    });

    window.addEventListener("load", () => {
        editModal.style.display = "none";
    });

    saveEditBtn?.addEventListener("click", () => {
        if (!selectedProject) {
            console.error("No project selected!");
            return;
        }

        const newTitle = editTitle.value.trim();
        const newDesc = editDesc.value.trim();
        const newTime = editTime.value.trim();
        const newImageSrc = editImage.src;

        if (newTitle && newDesc && newTime) {
            selectedProject.querySelector("h4").innerText = newTitle;
            selectedProject.querySelector(".desc").innerText = newDesc;
            selectedProject.querySelector(".budget-wrapper input").value = newTime;
            selectedProject.querySelector(".preview-image").src = newImageSrc;

            console.log("Changes saved:", newTitle, newDesc, newTime, newImageSrc);
            editModal.style.display = "none";
        } else {
            alert("Please fill all fields.");
        }
    });

    // === Open "Add Member" Modal from Project Dropdown ===
    document.querySelectorAll(".project-actions .add-member").forEach(button => {
        button.addEventListener("click", (e) => {
            e.preventDefault();

            const modal = document.getElementById("memberModal");

            if (!modal) {
                console.error("Error: Member modal not found!");
                return;
            }

            modal.style.display = "flex"; // Open modal
            console.log("✅ Add Member modal opened.");
        });
    });

    // === Close "Add Member" Modal ===
    const closeMemberModal = document.getElementById("closeMemberModal");
    if (closeMemberModal) {
        closeMemberModal.addEventListener("click", () => {
            document.getElementById("memberModal").style.display = "none";
            console.log("❌ Add Member modal closed.");
        });
    }

    // === Close Modal When Clicking Outside ===
    window.addEventListener("click", (event) => {
        const modal = document.getElementById("memberModal");
        if (event.target === modal) {
            modal.style.display = "none";
            console.log("❌ Add Member modal closed by outside click.");
        }
    });

    window.addEventListener("load", () => {
        document.getElementById("memberModal").style.display = "none";
    });
});



