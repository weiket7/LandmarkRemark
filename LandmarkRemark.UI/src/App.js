import "./App.css";
import React, { useRef, useState } from "react";
import { useEffect } from "react";
import "bootstrap/dist/css/bootstrap.min.css";
import Toast from "./components/Toast";
import getLocation from "./utilities/geoLocation"
import { getInfoWindow as initialiseSingleInfoWindow } from "./maps/InfoWindow"
import { addCurrentLocationMarker, addMarker, removeMarker } from "./maps/Marker"
import { getAllNotesApi, addNoteApi, searchNoteApi, deleteNoteApi } from "./utilities/noteApi";
import { appendMapScriptElement, getMap as initialiseMap, mapEnableAddMarker } from "./maps/Map";
import debounce from "./utilities/debounce";
import Textbox from "./components/Textbox";
import Button from "./components/Button";
import Alert from "./components/Alert";

function App() {
  let showToast;
  let map = useRef();

  const [notes, setNotes] = useState([]);
  const [isChangeUser, setIsChangeUser] = useState(true);
  const [inputUsername, setInputUsername] = useState("");
  const [username, setUsername] = useState();
  const [applicationError, setApplicationError] = useState(null);

  const initialiseApplication = () => {
    map.current = initialiseMap();

    initialiseSingleInfoWindow();

    const isLoggedIn = false;
    getAllNotes(isLoggedIn);

    var callback = (position) => {
      const pos = {
        lat: position.coords.latitude,
        lng: position.coords.longitude,
      };

      addCurrentLocationMarker(map.current, pos);
      map.current.setCenter(pos);
    }
    getLocation(callback);
  };
  window.initialiseApplication = initialiseApplication;

  window.closeInfoWindow = () => {
    window.infoWindow.close();
  };

  window.addNote = (latitude, longitude) => {
    const value = document.getElementById("txt-note").value;
    if(value.length === 0) {
      showToast("Remark is required", "danger");
      return;
    }

    const request = {username: username, remark: value, latitude: latitude, longitude: longitude };
    addNoteApi(request)
      .then(response => {
        let updatedNotes = [...notes];
        const note = response.data;

        const isLoggedIn = true;
        note.marker = addMarker(map.current, {lat: latitude, lng: longitude}, note, isLoggedIn);

        updatedNotes.push(note);
        setNotes(updatedNotes);

        window.closeInfoWindow();
        
        showToast("Note has been saved");
      });
  };

  window.deleteNote = (noteId) => {
    deleteNoteApi(noteId)
      .then(response => {
        const marker = notes.find(note=>note.noteId === noteId).marker;
        removeMarker(marker);

        let updatedNotes = [...notes];
        const index = updatedNotes.findIndex(note=>note.noteId === noteId);
        updatedNotes.splice(index, 1);
        setNotes(updatedNotes);

        window.closeInfoWindow();

        showToast("Note has been deleted");
      })
  };

  function search(searchTerm){
    searchNoteApi(searchTerm)
      .then(response => {
        setNotes(response.data);
      })
  }

  const enterSearchTerm = debounce((e) => search(e.target.value));

  const login = () => {
    if(inputUsername.length === 0) {
      showToast("Username is required", "danger");
      return;
    }
    setUsername(inputUsername);
    setInputUsername("");
    triggerChangeUser();
    
    mapEnableAddMarker(map.current);
    initialiseMarkers(notes, true);
  }

  const triggerChangeUser = () => {
    setIsChangeUser(!isChangeUser);
  }

  const getAllNotes = (isLoggedIn) => {
    getAllNotesApi()
      .then(response => {
        let notes = response.data;
        initialiseMarkers(notes, isLoggedIn);
      }).catch(error => {
        setApplicationError("API has error or is unavailable");
      })
  }

  const initialiseMarkers = (notes, isLoggedIn) => {
    notes.map(note=> note.marker = addMarker(map.current, {lat: note.latitude, lng: note.longitude}, note, isLoggedIn));
    setNotes(notes);
  }

  useEffect(() => {
    appendMapScriptElement();
  }, []);

  let UserHeader = isChangeUser ?
    (
      <div className="w-100 text-center header-user" style={{color:"white"}}>
        Username
        <Textbox change={(e) => setInputUsername(e.target.value)} disabled={applicationError} className="ms-2"></Textbox>
        <Button click={() => login()} className="ms-2" disabled={applicationError}>Log In</Button>
      </div>
    ) : (
    <div className="w-100 text-center header-user" style={{color:"white"}}>
      Logged in as: <b>{ username }</b>
      <Button click={() => triggerChangeUser()} className="ms-3">Change User</Button>
    </div>
  )

  return (
    <React.Fragment>
      <header className="navbar navbar-dark sticky-top bg-dark flex-md-nowrap p-0 shadow">
        <div className="navbar-brand col-md-3 col-lg-2 me-0 px-3">
          Landmark Remark
        </div>
        <button type="button" className="navbar-toggler position-absolute d-md-none collapsed"
          data-bs-toggle="collapse" data-bs-target="#sidebarMenu" aria-controls="sidebarMenu"
          aria-expanded="false" aria-label="Toggle navigation">
          <span className="navbar-toggler-icon"></span>
        </button>

        { UserHeader }
      </header>

      <Toast forwardRef={childShowToast => { showToast = childShowToast }} />

      <div className="container-fluid">
        <div className="row">
          <nav id="sidebarMenu" className="col-md-3 col-lg-2 d-md-block bg-light sidebar collapse">
            <div className="position-sticky overflow-auto pt-2" style={{height: "96vh"}}>
              <ul className="nav flex-column">
                <b>Search</b>
                <Textbox change={enterSearchTerm} disabled={applicationError}></Textbox>

                { notes.length === 0 ? <div>No results</div> : <table className="table">
                  <tbody>
                    { notes.map(note =>
                      <tr key={note.noteId}>
                        <td>
                          {note.remark}
                          <br/> <small><i>By {note.username}</i></small>
                        </td>
                      </tr>
                    )}
                  </tbody>
                </table>}
              </ul>
            </div>
          </nav>

          <main className="col-md-9 ms-sm-auto col-lg-10 px-md-4 mt-3">
            { applicationError ? <Alert colour="danger">{applicationError}</Alert> :
              username ? "" : <Alert>Please log in to save notes</Alert> }

            <div id="map"></div>
          </main>
        </div>
      </div>
    </React.Fragment>
  );
}

export default App;
