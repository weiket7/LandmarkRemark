import httpClient from "./axiosInstance"

export function getAllNotesApi() {
    return httpClient.get("note");
}

export function addNoteApi(request) {
    return httpClient.post("note/create", request);
}

export function deleteNoteApi(noteId) {
    return httpClient.post("note/delete", {noteId: noteId});
}

export function searchNoteApi(searchTerm) {
    return httpClient.get("note/search?term="+searchTerm);
}