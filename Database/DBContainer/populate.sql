BEGIN;

-- Insert users with movie-inspired details
INSERT INTO "Users" ("FirstName", "LastName", "Nickname", "Email", "Password") VALUES
                                                                                   ('admin', 'admin', 'admin', 'admin@domain.com', 'admin'),
                                                                                   ('Luke', 'Skywalker', 'jedi', 'luke@rebellion.com', 'theforce'),
                                                                                   ('Tony', 'Stark', 'ironman', 'tony@starkindustries.com', 'iamironman'),
                                                                                   ('Frodo', 'Baggins', 'ringbearer', 'frodo@shire.com', 'onering'),
                                                                                   ('Neo', 'Anderson', 'theone', 'neo@matrix.com', 'redpill'),
                                                                                   ('Ellen', 'Ripley', 'nostromo', 'ellen@weyland.com', 'inspace');

-- Make the first user (admin) an admin
INSERT INTO "Admins" ("UserId") VALUES (1);

-- Insert posts with movie easter egg references
INSERT INTO "Posts" ("Header", "Content", "UserId") VALUES
                                                        ('The Force Awakens', 'May the force be with you always.', 2),
                                                        ('I am Iron Man', 'Sometimes you gotta run before you can walk.', 3),
                                                        ('One Ring to Rule Them All', 'Even the smallest person can change the course of the future.', 4),
                                                        ('Follow the White Rabbit', 'There is no spoon, but there is the truth.', 5),
                                                        ('In Space, No One Can Hear You Scream', 'The battle for survival is just beginning.', 6),
                                                        ('You Shall Not Pass!', 'Admin privileges are granted to the wizard.', 1);

COMMIT;
