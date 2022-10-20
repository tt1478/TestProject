import React from "react";
import { Switch, Route, Link } from "react-router-dom";
import "bootstrap/dist/css/bootstrap.min.css";
import "@fortawesome/fontawesome-free/css/all.css";
import "@fortawesome/fontawesome-free/js/all.js";
import "./App.css";

import EmployeesList from "./components/EmployeesList";

function App() {
  return (
    <div>
      <nav className="navbar navbar-expand navbar-dark bg-dark">
        <a href="/employees" className="navbar-brand">
          TestProject
        </a>
        <div className="navbar-nav mr-auto">
        </div>
      </nav>

      <div className="container mt-3">
        <Switch>
          <Route exact path={["/", "/employees"]} component={EmployeesList} />
        </Switch>
      </div>
    </div>
  );
}

export default App;
