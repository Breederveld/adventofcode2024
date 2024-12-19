program prog
  Use, intrinsic :: iso_fortran_env, Only : iostat_end
  implicit none

  type PosQueue
    integer :: X
    integer :: Y
    integer :: Steps
    type (PosQueue), pointer :: next_ptr => null()
  end type PosQueue

  integer :: field(71, 71)
  integer :: input, x, y, width, height, i, step, err, halt
  type(PosQueue), pointer :: queueHead
  type(PosQueue), pointer :: queueTail

  width = size(field, 1)
  height = size(field, 2)
  halt = 1024

  do
    ! Initialize field
    do x = 1, size(field, 1)
      do y = 1, size(field, 2)
        field(x, y) = 999999
      end do
    end do

    ! read data from file 
    step = 0
    open(newunit=input, file = 'input.txt', status = 'old', action = 'read')
    do
      read(input,*,iostat=err) x, y
      if (err == iostat_end) exit
      field(x + 1, y + 1) = -1
      step = step + 1
      if (step >= halt) then
        print *, x, y
        exit
      end if
    end do  
    close(input) 

    ! Walk the field
    allocate(queueHead)
    queueTail => queueHead
    queueHead%X = 1
    queueHead%Y = 1
    queueHead%Steps = 0

    do while (associated(queueHead))
      x = queueHead%X
      y = queueHead%Y
      step = queueHead%Steps
      if (x > 0 .and. y > 0 .and. x <= width .and. y <= height .and. field(x, y) > step) then
        !print *, x, y, step
        field(x, y) = step
        allocate(queueTail%next_ptr)
        queueTail => queueTail%next_ptr
        queueTail%X = x + 1
        queueTail%Y = y
        queueTail%Steps = step + 1
        allocate(queueTail%next_ptr)
        queueTail => queueTail%next_ptr
        queueTail%X = x
        queueTail%Y = y + 1
        queueTail%Steps = step + 1
        allocate(queueTail%next_ptr)
        queueTail => queueTail%next_ptr
        queueTail%X = x - 1
        queueTail%Y = y
        queueTail%Steps = step + 1
        allocate(queueTail%next_ptr)
        queueTail => queueTail%next_ptr
        queueTail%X = x
        queueTail%Y = y - 1
        queueTail%Steps = step + 1
      end if
      queueHead => queueHead%next_ptr
    end do
    if (field(width, height) == 999999) then
      print *, "DONE"
      exit
    end if
    halt = halt + 1
  end do
end program prog
