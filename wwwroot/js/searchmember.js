const allMembers = [
    "Therése Lidbom", "Hans Mattin-Lassei", "John Doe", "Jane Smith",
    "Elina Berg", "Ali Reza", "Maria Gonzalez", "Tom Eriksson"
];

const memberInput = document.getElementById("editMemberSearch");
const memberResults = document.getElementById("editMemberResults");
const selectedMembersContainer = document.getElementById("editSelectedMembers");

let selectedMembers = [];

memberInput.addEventListener("input", () => {
    const val = memberInput.value.toLowerCase();
    memberResults.innerHTML = "";

    if (val.length > 0) {
        const filtered = allMembers
            .filter(name => name.toLowerCase().includes(val) && !selectedMembers.includes(name))
            .sort(() => 0.5 - Math.random())
            .slice(0, 3);

        if (filtered.length > 0) {
            filtered.forEach(name => {
                const div = document.createElement("div");
                div.textContent = name;
                div.classList.add("search-result");
                div.addEventListener("click", () => {
                    addMember(name);
                    memberInput.value = "";
                    memberResults.style.display = "none";
                });
                memberResults.appendChild(div);
            });
            memberResults.style.display = "block";
        } else {
            memberResults.style.display = "none";
        }
    } else {
        memberResults.style.display = "none";
    }
});

function addMember(name) {
    if (!selectedMembers.includes(name)) {
        selectedMembers.push(name);
        const tag = document.createElement("div");
        tag.className = "tag";
        tag.innerHTML = `${name} <span class="remove">✕</span>`;

        tag.querySelector(".remove").addEventListener("click", () => {
            selectedMembers = selectedMembers.filter(m => m !== name);
            tag.remove();
        });

        selectedMembersContainer.appendChild(tag);
    }
}

window.addEventListener("click", (e) => {
    if (!memberInput.closest(".member-search-box").contains(e.target)) {
        memberResults.style.display = "none";
    }
});

