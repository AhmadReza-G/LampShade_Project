﻿using _0_Framework.Application;
using AccountManagement.Application.Contracts.Role;
using AccountManagement.Domain.RoleAgg;

namespace AccountManagement.Application;
public class RoleApplication : IRoleApplication
{
    private readonly IRoleRepository _roleRepository;

    public RoleApplication(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public OperationResult Create(CreateRole command)
    {
        var operation = new OperationResult();

        if (_roleRepository.IsExists(x => x.Name == command.Name))
            return operation.Failed(ApplicationMessages.DuplicatedRecord);

        var role = new Role(command.Name);
        _roleRepository.Create(role);

        _roleRepository.SaveChanges();
        return operation.Succeded();
    }

    public OperationResult Edit(EditRole command)
    {
        var operation = new OperationResult();
        var role = _roleRepository.GetBy(command.Id);
        if (role is null)
            return operation.Failed(ApplicationMessages.RecordNotFound);

        if (_roleRepository.IsExists(x => x.Name == command.Name && x.Id != command.Id))
            return operation.Failed(ApplicationMessages.DuplicatedRecord);

        role.Edit(command.Name);

        _roleRepository.SaveChanges();
        return operation.Succeded();
    }

    public EditRole GetDetails(long id)
    {
        return _roleRepository.GetDetails(id);
    }

    public List<RoleViewModel> List()
    {
        return _roleRepository.List();
    }
}
