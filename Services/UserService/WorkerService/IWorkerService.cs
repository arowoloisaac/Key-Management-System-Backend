﻿using Key_Management_System.DTOs.UserDto.WorkerDto;

namespace Key_Management_System.Services.UserService.WorkerService
{
    public interface IWorkerService
    {
        Task RegisterWorker(RegisterWorkerDto workerDto);
    }
}
