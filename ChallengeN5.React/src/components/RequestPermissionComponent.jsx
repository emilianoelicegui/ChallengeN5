import React, { useEffect, useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import {
  Button,
  TextField,
  Select,
  MenuItem,
  FormControl,
  InputLabel,
  Grid,
  Typography,
  Container,
} from '@mui/material';
import {
  getAllPermissions,
  modifyPermission,
  requestPermission,
  resetEditPermission,
} from '../reducers/permissionSlice';
import { DatePicker } from '@mui/x-date-pickers';
import { Link, useNavigate, useParams } from 'react-router-dom';

const initForm = {
  id: 0,
  nombreEmpleado: '',
  apellidoEmpleado: '',
  tipoPermiso: '0',
  fechaPermiso: new Date()
};

function RequestPermissionComponent() {
  const dispatch = useDispatch();
  const { id } = useParams();
  const navigate = useNavigate();
  const {selectPermission, loading} = useSelector(state => state.permissions);
  const [idPermission, setIdPermission] = useState(0);
  const [nombreEmpleado, setNombreEmpleado] = useState('');
  const [apellidoEmpleado, setApellidoEmpleado] = useState('');
  const [tipoPermiso, setTipoPermiso] = useState('0');
  const [fechaPermiso, setFechaPermiso] = useState(null);

  useEffect(() => {
    async function fetchPermission() {
      if (id !== null && id > 0){
        dispatch(requestPermission(id));
      }
      else {
        dispatch(resetEditPermission());
      }
    }
    
    fetchPermission();
  }, [dispatch]);

  useEffect(() => {
    setIdPermission(selectPermission.id);
    setNombreEmpleado(selectPermission.nombreEmpleado);
    setApellidoEmpleado(selectPermission.apellidoEmpleado);
    setTipoPermiso(selectPermission.tipoPermiso);
    setFechaPermiso(new Date(selectPermission.fechaPermiso));
  }, [selectPermission]);

  function showLoading (){
    return <>
      Cargando ...
    </>
  }

  function setDefaultValues () {
    setNombreEmpleado(initForm.nombreEmpleado);
    setApellidoEmpleado(initForm.apellidoEmpleado);
    setTipoPermiso(initForm.tipoPermiso);
    setFechaPermiso(initForm.fechaPermiso);
  }

  function toHome(){
    //getAllPermissions();
    return navigate("/");
  }

  const handleRequestPermission = async () => {
    const newPermission = {
      id: idPermission,
      nombreEmpleado,
      apellidoEmpleado,
      tipoPermiso,
      fechaPermiso: fechaPermiso.toISOString()
    };

    dispatch(modifyPermission(newPermission));
    setDefaultValues();    
    toHome();
  };

  return (
    <Container maxWidth="sm">
      { loading ? (
        showLoading()
      ) : (
        <>
        <Typography variant="h5" gutterBottom>
        { id > 0 ? 'Actualizar Permiso' : 'Nuevo Permiso' }
      </Typography>
      <Grid container spacing={2}>
        <Grid item xs={12}>
          <TextField
            label="Nombre Empleado"
            fullWidth
            value={nombreEmpleado}
            onChange={(e) => setNombreEmpleado(e.target.value)}
          />
        </Grid>
        <Grid item xs={12}>
          <TextField
            label="Apellido Empleado"
            fullWidth
            value={apellidoEmpleado}
            onChange={(e) => setApellidoEmpleado(e.target.value)}
          />
        </Grid>
        <Grid item xs={12}>
          <FormControl fullWidth>
            <InputLabel>Tipo Permiso</InputLabel>
            <Select
              value={tipoPermiso}
              onChange={(e) => setTipoPermiso(e.target.value)}
            >
              <MenuItem value="0" disabled>
                Seleccione
              </MenuItem>
              <MenuItem value="1">Administrador</MenuItem>
              <MenuItem value="2">Empleado</MenuItem>
            </Select>
          </FormControl>
        </Grid>
        <Grid item xs={12}>
          <DatePicker
            label="Fecha Permiso"
            fullWidth
            value={fechaPermiso}
            onChange={(date) => setFechaPermiso(date)}
            renderInput={(params) => <TextField {...params} />}
          />
        </Grid>
        <Grid item xs={12}>
          <Link to="/">
          <Button variant="contained">
            Cancelar
          </Button>
          </Link>
          <Button variant="contained" onClick={handleRequestPermission}>
            Guardar
          </Button>
        </Grid>
      </Grid> 
      </>         
      )}
    </Container>
  );
}

export default RequestPermissionComponent;
