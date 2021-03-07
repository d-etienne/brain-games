PYLINT = flake8

FORCE:

tests: FORCE
	$(PYLINT) *.py
	nosetests --exe --with-coverage --verbose --cover-package=brain-games

prod: tests
	git commit -a
	git push origin main

%.py: FORCE
	nosetests tests.test_$* --nocapture
