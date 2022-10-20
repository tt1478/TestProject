import React, { useState, useEffect, useMemo, useRef } from "react";
import EmployeeService from "../services/EmployeeService";
import JobService from "../services/JobService";
import { useTable } from "react-table";
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import Modal from 'react-bootstrap/Modal';
import Button from 'react-bootstrap/Button';
import Select from 'react-dropdown-select';

const EmployeesList = (props) => {
    const initialEmployeeState = {
        id: null,
        fullName: "",
        phoneNumber: "",
        JobId: null
    };
    const [employees, setEmployees] = useState([]);
    const [searchName, setSearchName] = useState("");
    const [isOpened, setIsOpened] = useState(false);
    const [employee, setEmployee] = useState(initialEmployeeState);
    const [jobs, setJobs] = useState([]);
    const [selectedJob, setSeletedJob] = useState([{ value: 0, label: '' }]);
    const [isAdd, setIsAdd] = useState(false);
    const [fullNameValidationMessage, setFullNameValidationMessage] = useState("");
    const [phoneNumberValidationMessage, setPhoneNumberValidationMessage] = useState("");
    var jobsTemp = [{ value: 0, label: '' }];
    const employeesRef = useRef();

    employeesRef.current = employees;

    useEffect(() => {
        retrieveEmployees();
    }, []);

    const onChangeSearchName = (e) => {
        const searchName = e.target.value;
        setSearchName(searchName);
    };

    const retrieveEmployees = async () => {
        await EmployeeService.getAll()
            .then((response) => {
                setEmployees(response.data);
            })
            .catch((e) => {
                toast.warning(e, {
                    position: toast.POSITION.TOP_RIGHT
                });
            });
    };

    const findByName = async () => {
        await EmployeeService.findByName(searchName)
            .then((response) => {
                setEmployees(response.data);
            })
            .catch((e) => {
                toast.warning(e, {
                    position: toast.POSITION.TOP_RIGHT
                });
            });
    };
    const AddEmployee = () => {
        retrieveJobs();
        setEmployee({ id: null, fullName: "", phoneNumber: "", JobId: null });
        setSeletedJob([{ value: 0, label: '' }]);
        setFullNameValidationMessage("");
        setPhoneNumberValidationMessage("");
        setIsAdd(true);
        setIsOpened(true);

        //props.history.push("/add");
    }
    const openEmployee = (rowIndex) => {
        const id = employeesRef.current[rowIndex].id;
        retrieveJobs();
        getEmployee(id);
        setFullNameValidationMessage("");
        setPhoneNumberValidationMessage("");
        setIsAdd(false);
        setIsOpened(true);
        //props.history.push("/employees/" + id);
    };
    const getEmployee = async (id) => {
        await EmployeeService.get(id)
            .then(response => {
                setEmployee(response.data);
                debugger
                if (response.data.jobId > 0) {
                    debugger
                    var selectedJobTemp = jobsTemp.filter((data) => data.value === response.data.jobId);
                    setSeletedJob(selectedJobTemp);
                }
            })
            .catch(e => {
                toast.warning(e, {
                    position: toast.POSITION.TOP_RIGHT
                });
            });
    };
    const deleteEmployee = async (rowIndex) => {
        const id = employeesRef.current[rowIndex].id;

        await EmployeeService.remove(id)
            .then((response) => {
                props.history.push("/employees");

                let newEmployees = [...employeesRef.current];
                newEmployees.splice(rowIndex, 1);

                setEmployees(newEmployees);
                toast.success('Employee wa deleted successfuly', {
                    position: toast.POSITION.TOP_RIGHT
                });
            })
            .catch((e) => {
                toast.warning(e, {
                    position: toast.POSITION.TOP_RIGHT
                });
            });
    };
    const retrieveJobs = async () => {
        await JobService.getAll()
            .then((response) => {
                if (response.data) {
                    debugger
                    jobsTemp = [{ value: 0, label: '' }];
                    response.data.forEach(data => jobsTemp.push({ value: data.id, label: data.description }));
                    setJobs(jobsTemp);
                }
            })
            .catch((e) => {
                toast.warning(e, {
                    position: toast.POSITION.TOP_RIGHT
                });
            });
    };
    const handleInputChange = event => {
        const { name, value } = event.target;
        setEmployee({ ...employee, [name]: value });
    };
    const saveEmployee = async () => {
        if (validateForm() === false) {
            var data = {
                fullName: employee.fullName,
                phoneNumber: employee.phoneNumber,
                jobId: selectedJob[selectedJob.length - 1].value
            };

            await EmployeeService.create(data)
                .then(response => {
                    setEmployee({
                        id: response.data.id,
                        fullName: response.data.fullName,
                        phoneNumber: response.data.phoneNumber,
                        jobId: response.data.jobId
                    });
                    retrieveEmployees();
                    toast.success('Employee was created successfuly', {
                        position: toast.POSITION.TOP_RIGHT
                    });
                    setIsOpened(false);
                })
                .catch(e => {
                    toast.warning(e, {
                        position: toast.POSITION.TOP_RIGHT
                    });
                });
        }
    };
    const updateEmployee = async () => {
        if (validateForm() === false) {
            employee.jobId = selectedJob[selectedJob.length - 1].value;
            await EmployeeService.update(employee.id, employee)
                .then(response => {
                    retrieveEmployees();
                    toast.success('Employee was updated successfully', {
                        position: toast.POSITION.TOP_RIGHT
                    });
                    setIsOpened(false);
                })
                .catch(e => {
                    toast.warning(e, {
                        position: toast.POSITION.TOP_RIGHT
                    });
                });
        }
    };
    const validateForm = () => {
        if (employee.fullName.trim() === "" || employee.phoneNumber.trim() === "" || employee.phoneNumber.length !== 9) {
            if (employee.fullName === "") {
                setFullNameValidationMessage("Full name is required");
            }
            if (employee.phoneNumber === "") {
                setPhoneNumberValidationMessage("Phone number is required");
            }
            else if (employee.phoneNumber.length !== 9) {
                setPhoneNumberValidationMessage("Phone number should be 9 digits");
            }
            return true;
        }
        else {
            return false;
        }
    }
    const columns = useMemo(
        () => [
            {
                Header: "Full Name",
                accessor: "fullName",
            },
            {
                Header: "Phone Number",
                accessor: "phoneNumber",
            },
            {
                Header: "JobId",
                accessor: "job.description",
            },
            {
                Header: "Actions",
                accessor: "actions",
                Cell: (props) => {
                    const rowIdx = props.row.id;
                    return (
                        <div>
                            <span onClick={() => openEmployee(rowIdx)}>
                                <i className="far fa-edit action mr-2"></i>
                            </span>

                            <span onClick={() => deleteEmployee(rowIdx)}>
                                <i className="fas fa-trash action"></i>
                            </span>
                        </div>
                    );
                },
            },
        ],
        []
    );

    const {
        getTableProps,
        getTableBodyProps,
        headerGroups,
        rows,
        prepareRow,
    } = useTable({
        columns,
        data: employees,
    });

    return (
        <>
            <div className="list row">
                <div className="col-md-8">
                    <div className="input-group mb-3">
                        <input
                            type="text"
                            className="form-control"
                            placeholder="Search by Name"
                            value={searchName}
                            onChange={onChangeSearchName}
                        />
                        <div className="input-group-append">
                            <Button
                                variant="secondary"
                                onClick={findByName}
                            >
                                Search
                            </Button>
                        </div>
                    </div>
                </div>
                <div className="col-md-2"></div>
                <div className="col-md-2">
                    <Button
                        variant="primary"
                        onClick={AddEmployee}
                        style={{ float: "right" }}
                    >
                        Add Employee
                    </Button>
                </div>
                <div className="col-md-12 list">
                    <table
                        className="table table-striped table-bordered"
                        {...getTableProps()}
                    >
                        <thead>
                            {headerGroups.map((headerGroup) => (
                                <tr {...headerGroup.getHeaderGroupProps()}>
                                    {headerGroup.headers.map((column) => (
                                        <th {...column.getHeaderProps()}>
                                            {column.render("Header")}
                                        </th>
                                    ))}
                                </tr>
                            ))}
                        </thead>
                        <tbody {...getTableBodyProps()}>
                            {rows.map((row, i) => {
                                prepareRow(row);
                                return (
                                    <tr {...row.getRowProps()}>
                                        {row.cells.map((cell) => {
                                            return (
                                                <td {...cell.getCellProps()}>{cell.render("Cell")}</td>
                                            );
                                        })}
                                    </tr>
                                );
                            })}
                        </tbody>
                    </table>
                </div>

            </div>
            <ToastContainer />
            <Modal show={isOpened} onHide={() => setIsOpened(false)}>
                <Modal.Header closeButton>
                    <Modal.Title>Modal heading</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <div className="submit-form">
                        <div>
                            <div className="form-group">
                                <label htmlFor="fullName">Full Name</label>
                                <input
                                    type="text"
                                    className="form-control"
                                    id="fullName"
                                    required
                                    value={employee.fullName}
                                    onChange={handleInputChange}
                                    name="fullName"
                                />
                                <div className="text-danger">{fullNameValidationMessage}</div>
                            </div>

                            <div className="form-group">
                                <label htmlFor="phoneNumber">Phone Number</label>
                                <input
                                    type="text"
                                    className="form-control"
                                    id="phoneNumber"
                                    required
                                    value={employee.phoneNumber}
                                    onChange={handleInputChange}
                                    name="phoneNumber"
                                />
                                <div className="text-danger">{phoneNumberValidationMessage}</div>
                            </div>
                            <div className="form-group">
                                <label htmlFor="job">Job</label>
                                <Select
                                    options={jobs}
                                    searchable={true}
                                    multi={false}
                                    values={selectedJob}
                                    onChange={(value) => setSeletedJob(value)}
                                />
                            </div>
                        </div>
                    </div>
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={() => setIsOpened(false)}>
                        Close
                    </Button>
                    <Button variant="primary" onClick={isAdd ? saveEmployee : updateEmployee}>
                        Save Changes
                    </Button>
                </Modal.Footer>
            </Modal>
        </>
    );
};

export default EmployeesList;
