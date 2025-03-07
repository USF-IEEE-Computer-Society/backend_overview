BEGIN;

-- Insert more users with movie-inspired details
INSERT INTO "Users" ("FirstName", "LastName", "Nickname", "Email", "Password") VALUES
                                                                                   ('admin', 'admin', 'admin', 'admin@domain.com', 'admin'),
                                                                                   ('Luke', 'Skywalker', 'jedi', 'luke@rebellion.com', 'theforce'),
                                                                                   ('Tony', 'Stark', 'ironman', 'tony@starkindustries.com', 'iamironman'),
                                                                                   ('Frodo', 'Baggins', 'ringbearer', 'frodo@shire.com', 'onering'),
                                                                                   ('Neo', 'Anderson', 'theone', 'neo@matrix.com', 'redpill'),
                                                                                   ('Ellen', 'Ripley', 'nostromo', 'ellen@weyland.com', 'inspace'),
                                                                                   ('Bruce', 'Wayne', 'batman', 'bruce@wayneenterprises.com', 'darkknight'),
                                                                                   ('Diana', 'Prince', 'wonderwoman', 'diana@amazon.com', 'truthlasso'),
                                                                                   ('Jack', 'Sparrow', 'captain', 'jack@blackpearl.com', 'savvy'),
                                                                                   ('Peter', 'Parker', 'spiderman', 'peter@dailybugle.com', 'withgreatpower');

-- Make the first user (admin) an admin
INSERT INTO "Admins" ("UserId") VALUES (1);

-- Insert more posts for each user
INSERT INTO "Posts" ("Header", "Content", "UserId") VALUES
                                                        ('The Force Awakens', 'May the force be with you always.', 2),
                                                        ('Do or do not', 'There is no try.', 2),
                                                        ('I am Iron Man', 'Sometimes you gotta run before you can walk.', 3),
                                                        ('Genius, billionaire, playboy, philanthropist', 'That’s me.', 3),
                                                        ('One Ring to Rule Them All', 'Even the smallest person can change the course of the future.', 4),
                                                        ('It’s done', 'Sam, I am glad to be with you.', 4),
                                                        ('Follow the White Rabbit', 'There is no spoon, but there is the truth.', 5),
                                                        ('Wake up, Neo', 'The Matrix has you.', 5),
                                                        ('In Space, No One Can Hear You Scream', 'The battle for survival is just beginning.', 6),
                                                        ('I’m Batman', 'Because I’m Batman.', 7),
                                                        ('A hero can be anyone', 'Even someone putting a coat around a child.', 7),
                                                        ('The truth is all we have', 'Justice, strength, and wisdom.', 8),
                                                        ('I fight for those who cannot fight for themselves.', 'The Amazon way.', 8),
                                                        ('Where’s the rum?', 'Why is the rum always gone?', 9),
                                                        ('This is the day you will always remember', 'As the day you almost caught Captain Jack Sparrow!', 9),
                                                        ('With great power...', 'Comes great responsibility.', 10),
                                                        ('No way home', 'Sometimes the hardest thing is to accept the truth.', 10),
                                                        ('You Shall Not Pass!', 'Admin privileges are granted to the wizard.', 1),
                                                        ('Access Denied', 'Only admins can enter.', 1);

COMMIT;
