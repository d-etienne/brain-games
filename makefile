PYLINT = flake8
PYLINTFLAGS = --ignore=E741,W503 --exclude=interactive.py,__main__.py
PYTHONFILES = $(shell find . -name "*.py")

FORCE:

dev_env: FORCE
	pip3 install -r requirements-dev.txt
	python3 sendEmail.py

lint: $(patsubst %.py,%.pylint,$(PYTHONFILES))

tests: lint

%.pylint:
	$(PYLINT) $(PYLINTFLAGS) $*.py