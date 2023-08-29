import './App.css'
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom'
import GetPermissionsComponent from './components/GetPermissionsComponent'
import RequestPermissionComponent from './components/RequestPermissionComponent'

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<GetPermissionsComponent />} />
        <Route path="/request/:id?" element={<RequestPermissionComponent />} />
      </Routes>
    </Router>
  )
}

export default App;